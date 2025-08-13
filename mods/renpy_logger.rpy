init python:
    import os
    import time
    import re

    # --- CONFIG ---
    ENABLE_LABEL_LOGGING = False  # Toggle on/off internal label logging

    # --- Setup ---
    log_filename = os.path.join(config.basedir, "dialogue_log.txt")

    # Regex to strip inline tags like {fast}, {i}, {/i}
    tag_pattern = re.compile(r"\{.*?\}")

    def clean_text(t):
        """Remove inline Ren'Py text tags for duplicate check and logging."""
        return re.sub(tag_pattern, "", t).strip() if t else t

    def log_line(line_type, who, what):
        """Write a timestamped log line."""
        speaker = who if who else "Narrator"
        with open(log_filename, "a", encoding="utf-8") as f:
            f.write(f"[{line_type}] {speaker}: {what}\n")

    # --- SAY hook (dedupe + tag clean) ---
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

    # --- CHOICE overlay finder ---
    _last_choices = None

    def log_visible_choices_overlay():
        """
        Runs every frame; if 'choice' screen is visible, log current choices.
        Only logs when the active visible list changes to avoid spam.
        """
        global _last_choices

        si = renpy.display.screen.get_screen("choice")
        if si:
            ctx = getattr(si, "scope", {})
            if "items" in ctx:
                # Extract and clean captions from visible items
                caps = [clean_text(str(i.caption))
                        for i in ctx["items"]
                        if clean_text(str(i.caption))]
                if caps and caps != _last_choices:
                    for caption in caps:
                        log_line("CHOICE", None, caption)
                    _last_choices = caps

    # Register the overlay logger
    config.overlay_functions.append(log_visible_choices_overlay)