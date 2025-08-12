# logger.rpy
init python:
    import os
    import time
    import re

    # --- CONFIG ---
    ENABLE_LABEL_LOGGING = True  # <--- toggle this to True if you want label/call logging

    # --- Setup ---
    log_filename = os.path.join(config.basedir, "dialogue_log.txt")

    # Regex to strip Ren'Py inline text tags (like {fast}, {i}, etc.)
    tag_pattern = re.compile(r"\{.*?\}")

    def clean_text(t):
        """Remove inline Ren'Py text tags for duplicate check and logging."""
        return re.sub(tag_pattern, "", t).strip() if t else t

    def log_line(line_type, who, what):
        """Write a log line with timestamp and clean text."""
        ts = time.strftime("[%Y-%m-%d %H:%M:%S]")
        speaker = who if who else "Narrator"
        with open(log_filename, "a", encoding="utf-8") as f:
            f.write(f"{ts} [{line_type}] {speaker}: {what}\n")

    # --- SAY hook (no duplicates, strips tags before comparing) ---
    old_say = renpy.exports.say
    last_line = [None, None]  # [who, cleaned_text]

    def hooked_say(who, what, interact=True, **kwargs):
        if what and what.strip():
            clean_what = clean_text(what)
            if [who, clean_what] != last_line:
                log_line("SAY", who, clean_what)
                last_line[0], last_line[1] = who, clean_what
        return old_say(who, what, interact=interact, **kwargs)

    renpy.exports.say = hooked_say

    # --- MENU hook (supports any tuple/list length) ---
    old_menu = renpy.exports.menu

    def hooked_menu(items, *args, **kwargs):
        for entry in items:
            if isinstance(entry, (list, tuple)) and len(entry) >= 1:
                caption = entry[0]
            else:
                caption = str(entry)
            caption_clean = clean_text(str(caption))
            if caption_clean:
                log_line("CHOICE", None, caption_clean)
        return old_menu(items, *args, **kwargs)

    renpy.exports.menu = hooked_menu

    # --- LABEL logging (optional) ---
    if ENABLE_LABEL_LOGGING:
        old_jump = renpy.exports.jump
        old_call = renpy.exports.call

        def hooked_jump(label, *args, **kwargs):
            log_line("LABEL_JUMP", None, label)
            return old_jump(label, *args, **kwargs)

        def hooked_call(label, *args, **kwargs):
            log_line("LABEL_CALL", None, label)
            return old_call(label, *args, **kwargs)

        renpy.exports.jump = hooked_jump
        renpy.exports.call = hooked_call