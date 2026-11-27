# Lethe Chat - What Makes It Different

Most local LLM front-ends fall into one of two camps. **Roleplay front-ends** are great at characters, lorebooks and immersion, but treat tools and agents as an afterthought. **Assistant/agent front-ends** have solid function calling, skills and web search, but their "personality" is a system prompt and their memory, at best, is a generic vector store.

Lethe Chat is built to do both. It is a **persona-first** application, designed so a character can live with you for months and grow in a believable way. Under that persona, it still offers modern capabilities: native tool calling, skills, web and deep research, vision, and background agents.

This document explains how that works internally, in plain terms. All the heavy lifting is done by the [LetheAISharp](https://github.com/SerialKicked/Lethe-AI-Sharp) library, and Lethe Chat is the Windows application built around it.

---

## Table of Contents

1. [The Core Idea: Personas That Remember](#1-the-core-idea-personas-that-remember)
2. [The Multi-Tier Memory System](#2-the-multi-tier-memory-system)
3. [Topical Memory: RAG + Keywords](#3-topical-memory-rag--keywords)
4. [Memories With a Life Cycle](#4-memories-with-a-life-cycle)
5. [A Persona That Lives While You Are Away](#5-a-persona-that-lives-while-you-are-away)
6. [How the Prompt Is Assembled](#6-how-the-prompt-is-assembled)
7. [Tools, Skills and Agentic Features](#7-tools-skills-and-agentic-features)
8. [Effortless llama.cpp Management](#8-effortless-llamacpp-management)
9. [Characters, Groups and Roleplay Features](#9-characters-groups-and-roleplay-features)
10. [Transparency and Control](#10-transparency-and-control)
11. [Honest Limitations](#11-honest-limitations)

---

## 1. The Core Idea: Personas That Remember

An LLM has no memory of its own. Everything it "knows" about you has to fit in its context window, which is usually a few tens of thousands of tokens. Most chat programs handle this in one of two ways:

- They keep the most recent messages and silently drop everything older, so the character forgets.
- They bolt on a vector database that occasionally pastes a random old chunk of text into the prompt. That chunk is often out of context and only loosely related.

Lethe Chat models memory more like a person's:

- **Recent things** are remembered in full detail.
- **Last week** is remembered as a clear summary of each conversation.
- **Months ago** is remembered as broad strokes ("that was the month we talked about moving").
- **Stable facts about you** (your job, your pet's name, your preferences) are simply *known*, all the time.
- **Specific topics** come back to mind when they become relevant again, the way a word or a subject reminds you of something.

The result is a character that does not just recall facts on demand. It carries a sense of shared history: continuity, callbacks to older conversations, and knowledge of you that builds up over time.

---

## 2. The Multi-Tier Memory System

Conversations are divided into **sessions**, which are roughly one sitting or one day of chat. When a session ends, the library archives it and processes it into structured memory. From that point on, it feeds several memory layers, each with its own token budget so none of them can crowd out the others.

### Tier 1 - The live conversation

The current session is in the prompt in full. In *FitAll* mode (the default), the app also keeps as much of the previous sessions verbatim as the context window allows. Only what no longer fits gets handed down to the summary tiers.

### Tier 2 - Session summaries (recent past)

When a session is archived, one structured LLM pass extracts:

- a **title** and a **detailed summary**
- **keywords** (key topics)
- whether it was a **roleplay** session or a "real" conversation
- **facts** learned about the user
- a **relevance score**, meaning how important the session was

The summaries of the most recent sessions that no longer fit in the chat log are placed in the system prompt in chronological order. This gives the character a running diary of the last days or weeks.

### Tier 3 - Period summaries (earlier history)

Daily sessions pile up fast, and a week of summaries can already be a lot of text. So the library builds **meta-summaries** of closed calendar periods:

- **Weeks** are built from session summaries.
- **Months** are built from week summaries.
- **Quarters** are built from month summaries.

When the prompt is built, the history is read *backward in time and gets coarser as it goes*: recent sessions in detail, then a couple of weeks, then a couple of months, then as many quarters as the budget allows. The timeline is filled without gaps, and periods already covered by more detailed tiers are skipped. Period summaries are only rebuilt when their source sessions change.

This is what lets a persona say something like "back in spring, when you were stressed about your exams..." without the conversation ever having been in context.

### Tier 4 - Known facts (what the character simply knows about you)

On top of summaries, the library extracts short, single-sentence **facts** from every session. For example: "User works night shifts" or "User's cat is called Miso". Each fact is embedded on its own, and the library:

- **Deduplicates** them: when the same fact comes up again, its reference count goes up instead of creating a copy.
- **Supersedes** them: "User lives in Lyon" can replace an older "User lives in Paris". The old fact is retired rather than contradicting the new one.
- **Ranks** them by how often they come up and how recently. The top facts are always present in the system prompt (512-token budget by default).

Facts also act as a **retrieval shortcut**, as explained in the next section.

### Tier 5 - Topical memories

Everything else (old sessions, lorebook entries, research results, journal entries, goals, things the character chose to memorize) lives in a searchable memory vault. It is recalled only when the current conversation makes it relevant. That is the subject of the next section.

### One format for everything

Every memory, whatever its origin, is stored as the same `MemoryUnit` structure: a name, content, category, priority, insertion behavior, and an embedding. This is why session summaries, lorebook entries, web research and the character's own notes can all be searched, edited and browsed the same way.

---

## 3. Topical Memory: RAG + Keywords

"Topical" memory is about bringing the right thing back at the right moment. Lethe Chat combines two complementary approaches, because each one covers the other's weaknesses.

### Keyword triggers (lorebooks / World Info)

Users who know SillyTavern-style lorebooks will feel at home:

- Each entry has **main and secondary keywords**, combined with **And / Or / Not** logic.
- Matching is whole-word, over a configurable scan depth.
- Entries can have a **trigger chance** (for variety) and a **duration** (how many turns they stay active once triggered).
- Entries can be **sticky** (always present).
- Lorebooks can be attached per character, and SillyTavern World Info files can be imported.

Keywords are predictable, which makes them the right tool for names, places and anything with a specific word attached.

### Semantic search (RAG)

Keywords fail when the user talks *about* something without naming it. For that, the library uses embeddings:

- A local embedding model (**gte-large**, run through LLamaSharp inside the app) turns each memory into a vector. No external service is involved.
- Vectors are stored in an **HNSW** index (the same family of algorithm used by large vector databases), so search stays fast even with a lot of memories.
- Before searching, the user's message is **rewritten into the third person**, so "do you remember my sister?" becomes something closer to how the memory itself is phrased. This noticeably improves matching.
- Only results within a strict similarity threshold are kept, and only a few of them (top 3 by default). Weak or loose matches are discarded rather than forced into the prompt.
- Lorebook entries can **opt in** to semantic search, so they can be triggered without their keywords.

### The fact "two-hop" lookup

Session summaries cover many topics at once, which makes them hard to match precisely. A long summary that mentions your cat once among ten other subjects will not score well against "how's my cat doing?".

Known facts fix this. The message is first compared to the small, focused fact embeddings. If a fact matches, the library follows it back to the **session(s) it came from** and brings those in. A tiny, precise embedding opens the door to the full memory, much like a single cue can bring back a whole conversation.

### Roleplay-aware recall

Sessions are tagged as roleplay or not, and the character's brain adjusts recall based on whether you are currently in roleplay. A fictional adventure doesn't bleed into a real conversation about your day, and vice versa.

### Where memories land

Recalled memories are placed carefully so they don't break the flow:

- Retrieved past sessions are inserted a few messages before the end of the conversation, close to where they matter.
- Other recalled memories go either in a "relevant information" part of the system prompt or just above your latest message, depending on how they are configured.

---

## 4. Memories With a Life Cycle

A memory system that only ever grows becomes noisy. In Lethe Chat, memories have behaviors and lifespans.

### Insertion modes

| Mode | Behavior |
|------|----------|
| **Trigger** | Dormant until keywords or semantic search make it relevant. The default for stored knowledge. |
| **Natural** | A fresh insight (such as a research result) that the character is waiting to bring up. It surfaces when the conversation gets close to its topic, then becomes a normal Trigger memory. |
| **NaturalForced** | Like Natural, but brought up on its own after some time and messages, spaced out so it doesn't overwhelm the chat. |
| **UserReturn** | Mentioned once in the "welcome back" message when you return after being away. |
| **None** | Disabled. |

Natural memories (called "eureka" memories internally) are what let a character say *"Oh, by the way, I looked into that thing you mentioned..."* at the moment the topic comes back, instead of dumping it at the start of the next chat. Casual openers such as "what's up?" also give the character a chance to bring up something it has been holding on to.

### Decay and pruning

- Unused Natural insights expire after a while.
- Research results, goals and reminders **fade** if they are never recalled. Their lifespan grows with their priority and with how often they have been triggered, so important or frequently relevant memories survive much longer.
- Low-priority insights are discarded once used, and higher-priority ones are kept as long-term Trigger memories.
- Adding a memory that is nearly identical to an existing one replaces it instead of creating a duplicate.

### Future-dated memories

A memory can carry a date in the future and will not surface before that date. This is the basis for reminders and schedule-type features: the character can genuinely "remember" an appointment on the right day.

---

## 5. A Persona That Lives While You Are Away

A large part of what makes a persona feel real is that things happen *between* conversations. Lethe Chat has a background agent system that only runs when you have been inactive for a while (30 minutes by default) and the model is idle, so it never competes with your chat.

Tasks can be assigned **per character**:

- **Research tasks** look at the current or the last archived session, find topics the character could learn more about, search the web, merge the results into a coherent summary and store them as "eureka" memories. The next time you chat, the character can bring up what it found, in its own words.
- **Journal task** writes a daily first-person journal entry from the character's point of view. Over time, this gives the persona its own perspective on your shared history, not just a record of it.
- **Goal tasks** (goal designer, session goals, custom goals) give the character topics or intentions it wants to pursue in upcoming conversations. These expire naturally if they never come up.

When you come back after a longer absence (4 hours by default), the character gets a **welcome-back context**: how long you were gone, its schedule and mood (if enabled), notes from background tasks, and any memories set to come up on your return. A greeting after three days apart can then feel different from one after twenty minutes.

### Moods, schedules and personal notes

- **Mood system (optional):** moods are modular. Each mood is its own component, and they all work together to shape the character's behavior over time. Because moods are a plugin point rather than a fixed list, plugins can add new ones.
- **Daily schedule (optional):** the character can have a routine, which feeds into how it responds at different times of day.
- **Self-edited personal field:** the character can maintain its own notes about itself, which go into its system prompt.

---

## 6. How the Prompt Is Assembled

On every turn, the library rebuilds the full prompt from scratch. In simplified order:

1. **Context plugins** run first. They can adjust the input, add to the system prompt, or post-process output (the skills plugin and the anti-slop / formatting filters work this way).
2. **The character's brain** decides what to bring up, such as pending insights or a welcome-back message.
3. **Retrieval** runs: semantic search, the fact lookup, keyword triggers and sticky entries.
4. **The system prompt** is built in layers: base persona prompt, the character's personal notes, writing style, scenario, plugin text, important memories, earlier history (period summaries), previous sessions, known facts, tool instructions.
5. **The chat history** is added with the recalled memories placed in it.
6. **A final trim** removes the oldest messages until everything fits. On llama.cpp, it uses **exact token counts** from the server rather than estimates. Old tool-call chains and old images are also pruned to save space.

Every memory tier has its own **token budget**, so a long history never pushes out the conversation itself, and you can rebalance them in the settings.

---

## 7. Tools, Skills and Agentic Features

The persona focus does not come at the expense of capability. When the model and backend support it, Lethe Chat offers a full agent feature set.

### Native function calling

- Uses the backend's **native tool calling** (llama.cpp in chat mode, or any OpenAI-compatible server), not a fragile text-based imitation.
- **Parallel tool calls:** the model can request several tools in a single turn when the backend and model support it.
- **Multi-step tool loops:** the model can call tools, read the results and call more, up to a configurable limit (10 rounds by default). It is then asked to give its final answer.
- **Robust handling:** a tool call cut off mid-generation is retried instead of being executed half-written, and old tool chains are pruned from context automatically.
- **Tools can return images** to vision models (for example, "look at this file").
- **Confirmation:** tools that can affect your system require explicit approval. A dialog shows the tool name and its arguments, and you choose Allow or Deny. You can also require confirmation for every tool call.
- **Per-character toolsets:** each character can have its own set of allowed tools, so your roleplay companion doesn't need file access and your assistant doesn't need a lock timer.

### Built-in toolsets

- **Web Search:** web search (DuckDuckGo, or Brave with an API key), full page extraction, current date.
- **Memory Tools:** the model can save memories on its own, search them (also by date), and create reminders and schedule entries. The persona can decide by itself that something is worth remembering.
- **Media:** viewing images.
- **Skills:** see below.
- **App tools:** a drawing canvas, sandboxed file tools (limited to a workspace folder, with confirmation before opening anything), location, and a timer/lock plugin.
- **Plugins:** additional toolsets can be loaded from DLLs placed in the `plugins/` folder.

### Skills

Lethe Chat supports **skills** in the same `skill-name/SKILL.md` format used by Claude Code and OpenCode, so existing skills can be reused.

- Skills are found by a mix of keyword and semantic search.
- With tool calling, the model can **search and load** skills on demand. It can also create or update them, if you allow it, with confirmation.
- Without tool calling, matching skills can be suggested or injected into the context automatically.
- A skill editor is included, and it can **draft a new skill from the current conversation**.
- Imported skills start disabled until you review them.

### Deep research

A dedicated Deep Search window runs a multi-round research process: plan, search, collect findings, synthesize, decide whether more rounds are needed, and write a final report.

### Vision and reasoning models

- Images can be dragged into the chat and sent to vision-capable models.
- Reasoning ("thinking") models are fully supported. Their reasoning is shown in collapsible blocks, thinking can be turned on or off, and a reasoning budget can be set.

---

## 8. Effortless llama.cpp Management

llama.cpp is the recommended backend, and Lethe Chat **fully manages it** from the frontend. Once the server path and your model folders are set up, you never have to touch a command line or batch file again.

### Setup

In the settings, enable *"Allow Lethe Chat to manage Llama.cpp directly"* and point the app to your `llama-server.exe`. ik_llama.cpp is supported too, through a dedicated option that leaves out the arguments it doesn't support.

### Model library

Add one or more folders, and Lethe Chat scans them for `.gguf` models and builds a model library. Each model keeps **its own launch profile**:

- context size, GPU layers, threads, port
- KV cache quantization, flash attention, mlock / mmap
- reasoning on/off and reasoning budget
- the matching instruct template
- any extra command-line arguments for advanced use

### Automatic companions

When a model is selected, the app looks in its folder for:

- an **`mmproj` file**, which is loaded automatically to enable **vision**
- a **`.jinja` chat template**, which is loaded automatically to override the built-in template

A green indicator shows when either one was found.

### One-click launch

Pick a model and click connect. Lethe Chat then:

1. stops any server it launched before
2. starts llama-server in the background with that model's profile
3. shows the server log live, so you can see loading progress or errors
4. waits until the server is ready
5. connects, reads the server's capabilities (tool calling, vision, context size), and applies the model's instruct template

Switching models is a matter of picking another one from the list.

### Clean process handling

The llama-server process is tied to Lethe Chat through a Windows Job Object. If the app closes, or even crashes, the server is shut down with it. No orphaned servers keep holding your VRAM in the background.

### Why llama.cpp in particular

On llama.cpp, the library can use:

- **exact token counts** for context management (OpenAI-compatible servers only allow estimates)
- **native tool calling** and **vision**, detected automatically from the server
- **advanced samplers**
- **JSON schema / GBNF grammars**, used internally for the structured passes (session summaries, fact extraction, moods, journals)

Other backends remain supported: **KoboldCpp** (text completion, the only one with text-to-speech), any **OpenAI-compatible** server (LM Studio, Text Gen WebUI, and so on), and an **internal backend** that runs a GGUF model directly in the app.

---

## 9. Characters, Groups and Roleplay Features

- **Character editor:** a short bio, avatar, system prompt, preferred sampler settings, TTS voice, lorebooks, background tasks, allowed tools, mood and schedule options, and more, all per character.
- **Group chats:** several characters in one conversation. The next speaker can be chosen manually, in turn order, by name mention, or by the LLM itself, with a limit on how many bot replies happen in a row. The primary persona owns the shared history, and each character keeps its own memory.
- **Scenarios, system prompts, instruct formats and samplers** all have their own editors. Many instruct formats and sampler presets ship with the app.
- **Output quality filters:** an anti-slop phrase list, roleplay formatting fixes and optional emoji removal.
- **Privacy:** characters can be password-protected. Their chat logs and memories are then encrypted with AES-GCM. Without the password, the data cannot be recovered.

---

## 10. Transparency and Control

A complex memory system is only trustworthy if you can see what it does. Lethe Chat lets you look inside:

- **Memory Browser:** browse all memories and known facts, edit or delete them, and run a test semantic search to see what a sentence would recall.
- **Chat History Manager:** browse sessions, insert or delete them, regenerate summaries, re-embed everything and rebuild long-term period summaries.
- **Logs and Prompts:** view the **exact full prompt** sent to the model on the last turn, and a live, color-coded system log.
- **Status bar:** shows how full the context window is.
- **Slash commands:** for example `/recall` to check memory recall, `/sys` and `/skill`.

---

## 11. Honest Limitations

Lethe Chat is ambitious, and it is fair to be clear about where the edges are:

- **Model quality matters a lot.** The memory system relies on the model for summaries, fact extraction and following instructions. Small models (8B parameters or less) or weak instruction followers will produce noticeably worse memories.
- **Context size matters.** At least 10K tokens of context is needed for the memory tiers to work, and 16K or more is strongly recommended.
- **Archiving sessions takes time.** Ending a session triggers several LLM passes (summary, facts, period summaries). It takes anywhere from seconds to minutes depending on your hardware, and the first period-summary pass on a long existing history can take a while.
- **The embedding model is English-oriented.** gte-large works best in English, so semantic recall in other languages is weaker. Keyword triggers are not affected.
- **Tool calling requires a chat-mode backend.** It works with llama.cpp in chat mode and with OpenAI-compatible servers. It does not work with KoboldCpp, text-completion mode or the internal backend.
- **Text-to-speech is KoboldCpp-only.**
- **Model settings are yours to choose.** The app does not download models or pick GPU layers and context size automatically. You set them once per model, and the app takes care of the rest.
- **Windows only**, and designed for local or LAN backends rather than cloud APIs.
