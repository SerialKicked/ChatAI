# Changelog

## Unreleased

Requires the matching LetheAISharp update (see its changelog): the app relies on the new cancellation contract.

### Fixed
- The Cancel button is now reliable. It works at every stage of a message: while the app is "reading your message" (context plugins such as web search, memory, prompt building) as well as during generation, on llama.cpp (chat and text completion) and OpenAI API backends.
- The chat view no longer desyncs from the chat history after a cancel. Cancelling used to reload the whole page right away, which dropped the "is reading your message..." placeholder, and late streaming updates then overwrote the last real message on screen (often your own). Depending on the backend, the cancel could also make a message appear erased, or leave nothing behind at all.
- Streaming updates can no longer hit the wrong message: each update is addressed to the message being generated (by id), and dropped if that message is gone (cancelled, reloaded, or finalized).
- A rejected send / reroll / impersonate (engine busy) no longer leaves an orphan placeholder, a lost message, or a deleted one on screen. Send restores the typed text, and the page is rebuilt from the history.
- Double clicks or repeated Enter presses while a message is being prepared no longer start two turns.
- Cancelling a group chat now stops the whole response chain, including a member that was about to start.
- Cancelling an impersonation clears the input box.
- The UI no longer blocks the generation thread when the engine status changes (status updates are posted to the UI thread instead of invoked synchronously).

### Changed
- Cancel only asks the engine to stop. The partial response is discarded: nothing is added to the chat history, and the placeholder is removed from the page without a reload. Your sent message stays in the chat.
- Final message display: the placeholder becomes the history message once it is logged. If it is no longer on the page for any reason, the page is rebuilt from the history.
- Chat view updates (`WebUI`) are serialized. `ReloadFullChat` now waits for the page to finish loading, and rapid streaming updates for the same message are merged.
- `WebUI.EditMessage` takes an optional target message id. `WebUI.SendMessageToUI` has a `live` option marking the message being generated. New `WebUI.RemoveMessage(guid)` and `WebUI.LiveMessageGuid`.
- Group chat chain: the next member starts after the engine has released the previous turn, from the UI thread, instead of a fire-and-forget background task.
- The status bar shows "Cancelling..." and "Generation cancelled", and a message when the model is busy and a request was refused.

### Known limitations
- The tool-call confirmation dialog is not closed by Cancel.
- Cancelling a reroll leaves no assistant message (the old one was removed, the new one is discarded). Send with an empty input to generate a new reply.
- Web search / location plugin prompts built during the preparation step use estimated token counts, since the engine is already busy at that point.
