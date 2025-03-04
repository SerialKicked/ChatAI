# w(AI)fu.NET

Windows-based high performance front-end for KoboldCPP written in C#.NET. Using LLamaSharp for secondary functions like embed generation for RAG functionalities.

![WaifuAI_RjiqpbMiTq](https://github.com/user-attachments/assets/a4d1595d-a5f4-41f6-885e-5c50cd8fa619)

## Main Features:
- All in one program to edit characters, system prompts, instruction formats, and inference settings
- Chatlogs are divided into sessions, ability to switch back to previous session, insert or delete individual session
- Chat sessions are automatically summarised and formatted for easy access
- Compatible with R1 tyoe models and thinking tokens, implemented in non obstructive fashion
- Extensive **long-term memory (LTM) system**
  - Vector Search done on previous sessions to retrieve contextual information based on user prompt
  - Space can be reserved to insert the summaries of the last X sessions in chronological order, improving model's contextual awareness notably
  - Keyword-activated text insertion (or **World Info**) which can also be triggered using a keyword-less vector search
- Automatic insertion of dates into the prompt to give the model a better sense of time (toggle)
- TTS (text-to-speech) support through KoboldCPP API
- Ability for the bot to augment its responses by doing a search on DuckDuckGo through KoboldCPP API (toggle)
- Customizable system allowing the bot to browse user-defined websites in search of requested information
- Ability for the bot to initiate chat (toggle)
- Import chatlogs and worldinfo files from silly tavern
- Simple and intuitive UI

 ## Current Limitations:
 - No support for character cards, you'll have to copy paste info manually (no plans to implement)
 - 1v1 chat only, no group mode (yet)
 - No RAG support for external documents (yet)

## Notes and Requirements
- Runs on Windows 10+ (may work on 7, untested)
- A live, local, instance of KoboldCPP should be running (networked instance has not been tested)
- To take full advantage of the LTM system, a context window of at least 10K is recommanded. 16K is optimal.
- Small models (<=8B params), and models with poor instruction following, may struggle with some of the features.
