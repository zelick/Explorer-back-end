INSERT INTO payments."ShoppingCarts"(
	"Id", "UserId", "Items", "LastActivity", "Changes", "Version")
	VALUES (-1, -21, '[
	{{ "ItemId": -6, "Name": "Letovanje na Drini", "Price": 0, "Type": 0 }}, 
	{{ "ItemId": -2, "Name": "Obilazak beoradskih muzeja", "Price": 50, "Type": 0 }}
	]',
	current_timestamp, '[]', 0);

INSERT INTO payments."ShoppingCarts"(
	"Id", "UserId", "Items", "LastActivity", "Changes", "Version")
	VALUES (-2, -22, '[
	{{ "ItemId": -1, "Name": "Paket tura 1", "Price": 250, "Type": 1 }}
	]',
	current_timestamp, '[]', 0);

INSERT INTO payments."ShoppingCarts"(
	"Id", "UserId", "Items", "LastActivity", "Changes", "Version")
	VALUES (-3, -23, '[
	{{ "ItemId": -1, "Name": "Paket tura 1", "Price": 250, "Type": 1 }},
	{{ "ItemId": -2, "Name": "Obilazak beoradskih muzeja", "Price": 50, "Type": 0 }}
	]',
	current_timestamp, '[]', 0);
