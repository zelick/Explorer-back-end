INSERT INTO payments."Coupons"(
	"Id", "Code", "DiscountPercentage", "ExpirationDate", "IsGlobal", "TourId")
	VALUES (-1, 'ABC123BC', 10, to_timestamp('2024-10-24T12:00:00Z', 'YYYY-MM-DD"T"HH24:MI:SS"Z"'), TRUE, null);

INSERT INTO payments."Coupons"(
	"Id", "Code", "DiscountPercentage", "ExpirationDate", "IsGlobal", "TourId")
	VALUES (-2, '123ABCBC', 20, to_timestamp('2024-11-24T12:00:00Z', 'YYYY-MM-DD"T"HH24:MI:SS"Z"'), FALSE, -2);

INSERT INTO payments."Coupons"(
	"Id", "Code", "DiscountPercentage", "ExpirationDate", "IsGlobal", "TourId")
	VALUES (-3, 'EDFGCQ45', 10, to_timestamp('2024-12-24T12:00:00Z', 'YYYY-MM-DD"T"HH24:MI:SS"Z"'), FALSE, -3);