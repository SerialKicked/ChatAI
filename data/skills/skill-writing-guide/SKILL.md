---
name: skill-writing-guide
description: How to write a good skill. Use this before creating or rewriting a skill (with Skill_Create or Skill_Update) so that it is concise, reusable and easy to follow.
keywords: write a skill, create a skill, new skill, update skill, save procedure
---

# Writing a good skill

A skill is a short markdown document that teaches you how to carry out one kind of task. It is loaded later, in a different conversation, by someone who has not seen the current one.

## Rules

1. **One skill, one task.** If it covers two unrelated procedures, make two skills.
2. **Name**: lowercase words separated by hyphens, describing the task (`write-commit-messages`, not `skill1`).
3. **Description**: one or two sentences saying what the skill does AND when to use it. It is the only thing shown in the skill list, so include the situations or phrases that should trigger it.
4. **Content**: write instructions directly to the assistant, in the imperative. Use numbered steps for procedures.
5. **Generalize.** Remove names, dates and one-off details unless they are parameters of the procedure.
6. **Stay honest.** Only record steps and facts that were actually established. Do not invent anything.
7. **Keep it short.** A skill should rarely exceed a page. Long reference material belongs in memories, not skills.

## Suggested layout

```
## When to use
(the situations where this skill applies)

## Steps
1. ...
2. ...

## Notes and pitfalls
- (mistakes to avoid, edge cases)
```
