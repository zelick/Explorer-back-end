INSERT INTO stakeholders."Messages"(
	"Id", "SenderId", "RecipientId", "SenderUsername", "Title", "SentDateTime", "ReadDateTime", "Content", "IsRead")
	VALUES (-1, -13, -11, 'autor3@gmail.com', 'Naslov1', to_timestamp('2023-10-24T12:00:00Z', 'YYYY-MM-DD"T"HH24:MI:SS"Z"'), null, 'Pozdrav1', FALSE);
INSERT INTO stakeholders."Messages"(
	"Id", "SenderId", "RecipientId", "SenderUsername", "Title", "SentDateTime", "ReadDateTime", "Content", "IsRead")
	VALUES (-2, -21, -13, 'turista1@gmail.com', 'Naslov2', to_timestamp('2023-11-12T12:00:00Z', 'YYYY-MM-DD"T"HH24:MI:SS"Z"'), to_timestamp('2023-11-13T12:00:00Z', 'YYYY-MM-DD"T"HH24:MI:SS"Z"'), 'Pozdrav2', TRUE);
INSERT INTO stakeholders."Messages"(
	"Id", "SenderId", "RecipientId", "SenderUsername", "Title", "SentDateTime", "ReadDateTime", "Content", "IsRead")
	VALUES (-3, -12, -13, 'autor2@gmail.com', 'Naslov3', to_timestamp('2023-11-10T12:00:00Z', 'YYYY-MM-DD"T"HH24:MI:SS"Z"'), null, 'Pozdrav3', FALSE);
INSERT INTO stakeholders."Messages"(
	"Id", "SenderId", "RecipientId", "SenderUsername", "Title", "SentDateTime", "ReadDateTime", "Content", "IsRead")
	VALUES (-4, -11, -13, 'autor1@gmail.com', 'Naslov4', to_timestamp('2023-11-10T12:00:00Z', 'YYYY-MM-DD"T"HH24:MI:SS"Z"'), null, 'Pozdrav4', FALSE);