INSERT INTO blog."BlogPosts"(
    "Id", "UserId", "Title", "Description", "CreationDate", "ImageUrls", "Status")
VALUES (
    -1,
    -1,
    'Title 1',
    'Description 1',
    '2023-10-01 10:00:00',
    ARRAY['image1.jpg', 'image2.jpg'],
    0
);

INSERT INTO blog."BlogPosts"(
    "Id", "UserId", "Title", "Description", "CreationDate", "ImageUrls", "Status", "Comments")
VALUES (
    -11,
    -1,
    'Title 2',
    'Description 2',
    '2023-10-02 12:30:00',
    ARRAY['image11.jpg', 'image12.jpg'],
    1,
     (
        SELECT JSONB_AGG(subq.comment_row)
        FROM (
            SELECT
                JSONB_BUILD_OBJECT(
                    'UserId', -1,
                    'CreationTime', '2023-02-17T06:30:00',
                    'Text', 'Sample comment'
                ) AS comment_row
            UNION ALL
            SELECT
                JSONB_BUILD_OBJECT(
                    'UserId', -11,
                    'CreationTime', '2023-02-18T07:30:00',
                    'Text', 'Another comment'
                ) AS comment_row
        ) subq
    )
);

INSERT INTO blog."BlogPosts"(
    "Id", "UserId", "Title", "Description", "CreationDate", "ImageUrls", "Status", "Comments")
VALUES (
    -12,
    -1,
    'Title 3',
    'Description 3',
    '2023-10-03 12:30:00',
    ARRAY['image21.jpg', 'image22.jpg'],
    1,
     (
        SELECT JSONB_AGG(subq.comment_row)
        FROM (
            SELECT
                JSONB_BUILD_OBJECT(
                    'UserId', -12,
                    'CreationTime', '2023-02-17T06:30:00',
                    'Text', 'Sample comment'
                ) AS comment_row
            UNION ALL
            SELECT
                JSONB_BUILD_OBJECT(
                    'UserId', -12,
                    'CreationTime', '2023-02-18T07:30:00',
                    'Text', 'Another comment'
                ) AS comment_row
        ) subq
    )
);

INSERT INTO blog."BlogPosts"(
    "Id", "UserId", "Title", "Description", "CreationDate", "ImageUrls", "Status")
VALUES (
    -2,
    -12,
    'Title 4',
    'Description 4',
    '2023-10-04 15:15:00',
    ARRAY['image31.jpg', 'image32.jpg'],
    2
);