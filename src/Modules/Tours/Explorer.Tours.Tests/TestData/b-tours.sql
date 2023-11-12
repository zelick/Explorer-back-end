INSERT INTO tours."Tours"(
    "Id", "AuthorId", "Name","Description","DemandignessLevel","Status","Price","Tags","PublishedTours","ArchivedTours","TourTimes")
VALUES (-1, 2, 'Obilazak Novog Sada','Gastronomska pešačka tura koja obuhvata obilazak poznatih restorana',0,0,100,ARRAY ['hrana','ns'], null, null, '[{{ "Distance": 1000, "TimeInSeconds": 74500, "Transportation": 0 }}]');
INSERT INTO tours."Tours"(
    "Id", "AuthorId", "Name","Description","DemandignessLevel","Status","Price","Tags","PublishedTours","ArchivedTours","TourTimes")
VALUES (-2, 2, 'Beograd','Obilazak beoradskih muzeja',1,0,0,ARRAY ['muzej','bg'], null, null, null);
INSERT INTO tours."Tours"(
    "Id", "AuthorId", "Name","Description","DemandignessLevel","Status","Price","Tags","PublishedTours","ArchivedTours","TourTimes")
VALUES (-3, 2, 'Zimovanje na Tari','Organizovan prevoz i smestaj',0,0,0,ARRAY ['tara'], null, null, null);

INSERT INTO tours."Tours"(
    "Id", "AuthorId", "Name","Description","DemandignessLevel","Status","Price","Tags","PublishedTours","ArchivedTours","TourTimes")
VALUES (-4, 2, 'Letovanje na Tari','Organizovan smestaj',0,1,0,ARRAY ['tara'], null, null, null);

INSERT INTO tours."Tours"(
    "Id", "AuthorId", "Name","Description","DemandignessLevel","Status","Price","Tags","PublishedTours","ArchivedTours","TourTimes")
VALUES (-5, 2, 'Letovanje na Tari','Organizovan smestaj',0,0,0,ARRAY ['tara'], null, null, '[{{ "Distance": 1000, "TimeInSeconds": 74500, "Transportation": 0 }}]');

INSERT INTO tours."Tours"(
    "Id", "AuthorId", "Name","Description","DemandignessLevel","Status","Price","Tags","PublishedTours","ArchivedTours","TourTimes")
VALUES (-6, 2, 'Letovanje na Tari','Organizovan smestaj',0,1,0,ARRAY ['tara'], null, null, '[{{ "Distance": 1000, "TimeInSeconds": 74500, "Transportation": 0 }}]');
