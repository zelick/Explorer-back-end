INSERT INTO tours."Checkpoints"(
	"Id", "TourId","AuthorId", "Longitude", "Latitude", "Name", "Pictures","RequiredTimeInSeconds","CheckpointSecret")
	VALUES (-1, -1, 2, 45, 45, 'Bulevar oslobodjenja', ARRAY ['https://www.google.com/url?sa=i&url=https%3A%2F%2Fwww.flickr.com%2Fphotos%2Faleksandarm021%2F26719027317&psig=AOvVaw1A688rUlw00Ce7T_34isza&ust=1697738898312000&source=images&cd=vfe&opi=89978449&ved=0CA8QjRxqFwoTCJio5rKYgIIDFQAAAAAdAAAAABAD'], 1000, null);

INSERT INTO tours."Checkpoints"(
	"Id", "TourId","AuthorId", "Longitude", "Latitude", "Name", "Description", "Pictures","RequiredTimeInSeconds","CheckpointSecret")
	VALUES (-2, -2,2, 45, 45, 'Fakultet tehnickih nauka', 'Zgrada fakulteta', ARRAY ['https://www.google.com/url?sa=i&url=http%3A%2F%2Fwww.ftn.uns.ac.rs%2F102839474%2Fdan-fakulteta-tehnickih-nauka&psig=AOvVaw1dKjDVSgmfCmiWmDAkOag9&ust=1697738987796000&source=images&cd=vfe&opi=89978449&ved=0CA8QjRxqFwoTCPjd_uaYgIIDFQAAAAAdAAAAABAD'], 50000, null);

	
INSERT INTO tours."Checkpoints"(
	"Id", "TourId","AuthorId", "Longitude", "Latitude", "Name", "Description", "Pictures","RequiredTimeInSeconds","CheckpointSecret")
	VALUES (-3, -2, 2, 45, 45, 'Promenada', 'Trzni centar', ARRAY ['https://www.google.com/url?sa=i&url=https%3A%2F%2Ffirestopsistem.rs%2Ftc-promenada-novi-sad%2F&psig=AOvVaw0Q0-xhx6cZMtna2IJrxZ4k&ust=1697739067875000&source=images&cd=vfe&opi=89978449&ved=0CA8QjRxqFwoTCOjcuoKZgIIDFQAAAAAdAAAAABAD'], 20900, null);

INSERT INTO tours."Checkpoints"(
	"Id", "TourId","AuthorId", "Longitude", "Latitude", "Name", "Description", "Pictures","RequiredTimeInSeconds","CheckpointSecret")
	VALUES (-4, -1,2, 45, 45, 'Fakultet tehnickih nauka2', 'Zgrada fakulteta2', ARRAY ['https://www.google.com/url?sa=i&url=http%3A%2F%2Fwww.ftn.uns.ac.rs%2F102839474%2Fdan-fakulteta-tehnickih-nauka&psig=AOvVaw1dKjDVSgmfCmiWmDAkOag9&ust=1697738987796000&source=images&cd=vfe&opi=89978449&ved=0CA8QjRxqFwoTCPjd_uaYgIIDFQAAAAAdAAAAABAD'], 50000, null);
