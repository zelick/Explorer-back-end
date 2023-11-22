INSERT INTO stakeholders."Notifications"("Id", "Description", "CreationTime", "IsRead", "UserId", "Type", "ForeignId")
VALUES 
  (-1, 'Notification 1', CURRENT_TIMESTAMP, true, -12, 1, -1),
  (-2, 'Notification 2', CURRENT_TIMESTAMP, false, -12, 1, -1),
  (-3, 'Notification 3', CURRENT_TIMESTAMP, true, -11, 2, -1),
  (-4, 'Notification 4', CURRENT_TIMESTAMP, false, -11, 2, -1),
  (-5, 'Notification 5', CURRENT_TIMESTAMP, true, -12, 3, -1),
  (-6, 'Notification 6', CURRENT_TIMESTAMP, true, -11, 3, -1);
