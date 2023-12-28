INSERT INTO stakeholders."SecureTokens"(
	"Id", "UserId", "TokenData", "ExpiryTime", "IsAlreadyUsed")
	VALUES (-1, -11, 'usedTokenDataOfUser11', '2023-12-27T15:40:00', true);

INSERT INTO stakeholders."SecureTokens"(
	"Id", "UserId", "TokenData", "ExpiryTime", "IsAlreadyUsed")
	VALUES (-2, -11, 'unusedTokenDataOfUser11', '2023-12-27T19:40:00', false);

INSERT INTO stakeholders."SecureTokens"(
	"Id", "UserId", "TokenData", "ExpiryTime", "IsAlreadyUsed")
	VALUES (-3, -12, 'unusedTokenDataOfUser12', '2023-12-27T21:40:00', false);