# w(AI)fu.NET

Windows-based high performance front-end for KoboldCPP written in C#.NET.

![WaifuAI_RjiqpbMiTq](https://github.com/user-attachments/assets/a4d1595d-a5f4-41f6-885e-5c50cd8fa619)

## Main Features:
- All in one program to edit characters, system prompts, instruction formats, and inference settings
- Chatlogs are divided into sessions, ability to switch back to previous sessions, insert or delete individual session
- Chat sessions are automatically summarised and formatted for easy access
- Extensive **long-term memory system*
  - Vector Search done on previous sessions to retrieve contextual information
  - Space can be reserved to insert the summaries of the last X sessions in chronological order, improving models' awareness notably
  - Keyword-activated text insertion (or **World Info**) which can also be triggered using a keyword-less vector search
- Automatic insertion of dates into the prompt to give the model a better sense of time (toggle)
- TTS (text-to-speech) support through KoboldCPP API
- Ability for the bot to augment its responses by doing a search on DuckDuckGo through KoboldCPP API (toggle)
- Customizable system allowing the bot to browse user-defined websites in search of requested information
- Ability for the bot to initiate chat (toggle)
- Import chatlogs and worldinfo files from silly tavern
- Simple and intuitive UI

 ## Current Limitations:
 - 1v1 chat only, no group mode
 - No UI to edit characters (they are stored in a very legible json file, though)
 - No support for character cards, manual conversion only
