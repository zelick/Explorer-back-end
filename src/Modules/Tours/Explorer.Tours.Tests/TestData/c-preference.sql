INSERT INTO tours."TourPreference"(
    "Id", "CreatorId", "Difficulty", "Walk", "Bike", "Car", "Boat", "Tags")
VALUES 
  (-1, 2, 1, 1, 1, 1, 1, ARRAY['#streetfood', '#art']),
  (-2, 1, 0, 0, 0, 0, 0, ARRAY['#streetfood','#art']),
  (-3, 0, 2, 0, 0, 0, 0, ARRAY['#streetfood','#art']);
