# Lethe AI Chat

Lethe Chat is a Windows-based, high-performance, LLM harness and chat program, supporting a variety of backends like Llama.cpp, KoboldCpp, LMStudio, and more. It is written in C# .NET 10 and is using [Lethe AI Sharp](https://github.com/SerialKicked/Lethe-AI-Sharp) to do the heavy lifting.

<img width="1920" height="1032" alt="LetheChat-Dev_hhXQQa2tZO" src="https://github.com/user-attachments/assets/5f266867-e520-4c8a-b7fc-36b92afb6c1e" />

## ⭐ Main Features:

- All-in-one program to edit characters, prompts, instruction formats, and inference settings
- Chatlogs are divided into sessions, ability to switch back to previous sessions, insert or delete individual sessions
- Chat sessions are automatically summarised and formatted for easy access
- Extensive **long-term memory (LTM) system**
  - Vector Search done on previous sessions to retrieve contextual information based on user prompt
  - Space can be reserved to insert the summaries of the last X sessions in chronological order, improving the model's long-term memory 
  - Characters learn and remember facts about the user over time naturally through conversation without the need for manual input
  - Keyword-activated text insertion (or **WorldInfo/Lorebook**) which can also be triggered using a keyword-less vector search
  - Tool-calling models can even store and retrieve memories on their own
- Optional strong encryption of chatlogs and memories.
- **Agentic abilities**, allowing you to create characters that can perform specific tasks or functions
  -	Simple and Deep Web search via DuckDuckGo and Brave API
  - Extensible tool-calling, allowing the bot to interact with external programs and APIs to perform tasks or retrieve information
  - Extensible background tasks, allowing the bot to perform tasks while the user is AFK
- Detailed **character customisation** system, allowing you to create characters with unique personalities and knowledge bases
  - Per character system prompt, TTS voice, inference settings, tools, background tasks, and more
  - Optional evolving mood system, allowing characters to change behavior over time
  - Optional daily schedule, and point systems 
- Text-to-speech support (through KoboldCPP API only)
- Group chat support (multi-character conversations)  
- Customizable system allowing the bot to browse user-defined websites in search of requested information
- Simple and intuitive UI

## 🧩 Supported Backends
- **Llama.cpp:** Recommended, [Llama.cpp](https://github.com/ggml-org/llama.cpp) has the stronger feature-set and can be managed directly from LetheChat.
- **Kobold API:** Powerful text completion API, used by [KoboldCpp](https://github.com/LostRuins/koboldcpp).
- **OpenAI API:** Industry standard chat completion API, used by [LM Studio](https://lmstudio.ai/), [Text Gen WebUI](https://github.com/oobabooga/text-generation-webui), and many other backends.
- **Internal**: You can also run LetheChat without an external backend. While sacrificing some features, this allows you to load a model directly into LetheChat without the need for a separate backend.

This application is designed for local deployment. The backend must be running on your computer or on a local network.

## 🔎 Notes and Requirements
- Runs on Windows 10+
- To take full advantage of the LTM and dynamic character systems, a context window of at least 10K is necessary, 16K+ is *heavily* recommended
- Small models (<=8B params), and models with poor instruction following, may struggle with some of the features
- You can find a list of recommended models in my [Hugging Face Collections](https://huggingface.co/SerialKicked/collections)
- For TTS functionalities, KoboldCPP API is required

## License

Lethe AI Chat is under [CC BY-NC-SA 4.0](https://github.com/SerialKicked/ChatAI/tree/master?tab=License-1-ov-file#readme). You may not use this code for commercial purposes.
