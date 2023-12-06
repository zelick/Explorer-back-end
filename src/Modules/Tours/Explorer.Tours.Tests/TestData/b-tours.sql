INSERT INTO tours."Tours"(
    "Id", "AuthorId", "Name","Description","DemandignessLevel","Status","Price","Tags","PublishedTours","ArchivedTours","TourTimes","Closed")
VALUES (-1, -12, 'Obilazak Novog Sada','Gastronomska pešačka tura koja obuhvata obilazak poznatih restorana',0,0,100,ARRAY ['hrana','ns'], null, null, '[{{ "Distance": 1000, "TimeInSeconds": 74500, "Transportation": 0 }}]',false);
INSERT INTO tours."Tours"(
    "Id", "AuthorId", "Name","Description","DemandignessLevel","Status","Price","Tags","PublishedTours","ArchivedTours","TourTimes","Closed")
VALUES (-2, -12, 'Beograd','Obilazak beoradskih muzeja',1,0,0,ARRAY ['muzej','bg'], null, null, null,false);
INSERT INTO tours."Tours"(
    "Id", "AuthorId", "Name","Description","DemandignessLevel","Status","Price","Tags","PublishedTours","ArchivedTours","TourTimes","Closed")
VALUES (-3, -11, 'Zimovanje na Tari','Organizovan prevoz i smestaj',0,0,0,ARRAY ['tara'], null, null, null,false);

INSERT INTO tours."Tours"(
    "Id", "AuthorId", "Name","Description","DemandignessLevel","Status","Price","Tags","PublishedTours","ArchivedTours","TourTimes","Closed")
VALUES (-4, -12, 'Letovanje na Tari','Organizovan smestaj',0,1,0,ARRAY ['tara'], null, null, null,false);

INSERT INTO tours."Tours"(
    "Id", "AuthorId", "Name","Description","DemandignessLevel","Status","Price","Tags","PublishedTours","ArchivedTours","TourTimes","Closed")
VALUES (-5, -12, 'Letovanje na Tari','Organizovan smestaj',0,0,0,ARRAY ['tara'], null, null, '[{{ "Distance": 1000, "TimeInSeconds": 74500, "Transportation": 0 }}]',false);

INSERT INTO tours."Tours"(
    "Id", "AuthorId", "Name","Description","DemandignessLevel","Status","Price","Tags","PublishedTours","ArchivedTours","TourTimes","Closed")
VALUES (-6, -12, 'Letovanje na Tari','Organizovan smestaj',0,1,0,ARRAY ['tara'], null, null, '[{{ "Distance": 1000, "TimeInSeconds": 74500, "Transportation": 0 }}]',false);

INSERT INTO tours."Tours"(
    "Id", "AuthorId", "Name","Description","DemandignessLevel","Status","Price","Tags","PublishedTours","ArchivedTours","TourTimes","Closed")
VALUES (-7, -12, 'Obilazak Rume', 'Organizovana setnja',0,1, 1000,ARRAY ['tara'], null, null, '[{{ "Distance": 1000, "TimeInSeconds": 74500, "Transportation": 0 }}]',false);