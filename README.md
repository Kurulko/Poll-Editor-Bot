# Telegram bot for editing polls

* [Commands](#commands)
* [Errors](#errors)
<br/>

## Commands:

### To edit a poll, use these commands after sending a poll:<br/><br/>

• **/change_visibility** - edit poll visibility (public/anonymous):<br/>

![Screenshot 2024-01-24 173257](https://github.com/Kurulko/Poll-Editor-Bot/assets/95112563/36b5bdbd-304d-4120-b47b-2b053c7caad1)

• **/change_question** - edit poll question:<br/>

![Screenshot 2024-01-24 173459](https://github.com/Kurulko/Poll-Editor-Bot/assets/95112563/1fcd516b-c1a0-43c7-a257-54445e56de65)

• **/change_question_by_template** - edit poll question by template<br/>
• **/change_poll_type** - edit poll type:<br/>

 ![Screenshot 2024-01-24 173552](https://github.com/Kurulko/Poll-Editor-Bot/assets/95112563/8b21f00e-a3f9-421d-8fae-cc8f8ebf548f)

• **/change_correct_option** - edit poll's correct option (only for quizzes):<br/>

![Screenshot 2024-01-24 173707](https://github.com/Kurulko/Poll-Editor-Bot/assets/95112563/558fed21-dc5a-41c7-9371-feaa1f960cbe)

• **/change_explanation** - edit poll explanation (only for quizzes, optional):<br/>

![image](https://github.com/Kurulko/Poll-Editor-Bot/assets/95112563/959a1ea0-fc07-4f47-a6fe-a135e1c7dd52)

• **/drop_explanation** - remove poll explanation (only for quizzes, if present, optional)<br/>
• **/change_open_period** - edit poll's open period (optional)<br/>
• **/drop_open_period** - remove poll's open period (optional, if present)<br/>
• **/change_option** - edit a poll option:<br/>

![image](https://github.com/Kurulko/Poll-Editor-Bot/assets/95112563/bf6f12e5-86b5-4369-927f-92bbe4aba857)

• **/insert_option** - insert a poll option:<br/>

![image](https://github.com/Kurulko/Poll-Editor-Bot/assets/95112563/bd5024a0-6f0e-4879-89b1-a57cc7444882)

• **/add_option_to_end** - add a poll option to end"<br/>

![image](https://github.com/Kurulko/Poll-Editor-Bot/assets/95112563/69c60076-8657-474f-977c-282162d0a6a8)

• **/delete_option** - remove a poll option:<br/>

![image](https://github.com/Kurulko/Poll-Editor-Bot/assets/95112563/a69f23d4-2dde-4d8c-8e1e-3f0c6f841846)

• **/change_options** - edit all poll options"<br/>

![image](https://github.com/Kurulko/Poll-Editor-Bot/assets/95112563/94249b41-a7f7-48ed-9d41-bbca810aaca5)

• **/change_is_multiple_answers** - edit - multiple answers"<br/>

![image](https://github.com/Kurulko/Poll-Editor-Bot/assets/95112563/719187ac-0896-4f2c-bb0a-453d1859d05f)

#### For other commands that require a poll:

• **/get_text_poll** - get text poll<br/>

### No need to send a poll for these options:

• **/create_poll** - create a poll<br/>

#### Others:

• **/start** - start the bot<br/>
• **/stop** - stop the bot<br/>
• **/help** - view all bot commands<br/>

❗️**IMPORTANT**: The quiz must be submitted on your behalf and must be answered if it is not yours.<br/>
⚠️**WARNING**: Once the quiz is edited, all the answers will be discarded.<br/>
<br/>

## Errors:

• **MESSAGE_TYPE_NOT_SUITABLE** - the current message type is not suitable<br/>
• **MESSAGE_ENTITY_TYPE_NOT_SUPPORTED** - The message entity type is not supported for now<br/>
• **QUIZ_SENT_INCORRECTLY** - The quiz was sent incorrectly. The quiz must be submitted on your behalf and must be answered if it is not yours:<br/>

![Screenshot 2024-01-24 172959](https://github.com/Kurulko/Poll-Editor-Bot/assets/95112563/c879a351-4a88-4f0a-bc6f-b772abc32179)
