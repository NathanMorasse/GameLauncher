insert into `department` (`Name`, `Building`, `Floor`) 
values 
("Comptabilité", "F", 5),
("Informatique", "A", 5),
("Français", "F", 3);

insert into `room` (`Department_Id`, `Number`, `HasAirConditioning`, `HasHeaters`, `HasPhone`, `HasMovementSensor`) 
values 
(1, 5007, false, true, false, false),
(1, 5008, false, true, false, false),
(2, 5014, true, true, false, true),
(2, 5009, true, true, false, true),
(2, 5003, true, true, false, true),
(2, 5002, true, true, false, true),
(3, 3007, false, true, false, false),
(3, 3009, false, true, false, false),
(3, 3011, false, true, false, false);

insert into `furniture` (`Room_Id`, `Brand`, `Type`, `Description`, `Length`, `Height`, `Width`) 
values 
((select Id from room where Number = 5007), "NoName", "Burreau (F-5007)", "Burreau moyen et peu chère. Utilisé pour les étudiants.", 100, 150, 300),
((select Id from room where Number = 5007), "FlexChair", "Chaise (F-5007)", "Chaise classique d'école en simli plastique et en métal.", 50, 100, 50),
((select Id from room where Number = 5007), "Unknown", "Burreau Prof (F-5007)", "Burreau pour les professeurs situé à l'avant de la classe.", 100, 250, 200),

((select Id from room where Number = 5008), "NoName", "Burreau (F-5008)", "Burreau moyen et peu chère. Utilisé pour les étudiants.", 100, 150, 300),
((select Id from room where Number = 5008), "FlexChair", "Chaise (F-5008)", "Chaise classique d'école en simli plastique et en métal.", 50, 100, 50),
((select Id from room where Number = 5008), "Unknown", "Burreau Prof (F-5008)", "Burreau pour les professeurs situé à l'avant de la classe.", 100, 250, 200),

((select Id from room where Number = 5014), "IKEA", "Burreau arrondi (A-5014)", "Burreau arrondi en bois de taille moyenne. Parfait pour des poste informatique.", 100, 200, 300),
((select Id from room where Number = 5014), "Herman Miller", "Chaise (A-5014)", "Chaise ergonomique avec roulette.", 50, 150, 50),
((select Id from room where Number = 5014), "Ergotron", "Support à moniteur (A-5014)", "Support à moniteur double. Peut supporter plusieurs tailles de moniteur.", 10, 50, 10),

((select Id from room where Number = 5009), "IKEA", "Burreau (A-5009)", "Grand burreau en bois et en métal. Parfait pour des poste informatique.", 200, 200, 500),
((select Id from room where Number = 5009), "Herman Miller", "Chaise (A-5009)", "Chaise ergonomique avec roulette.", 50, 150, 50),
((select Id from room where Number = 5009), "Ergotron", "Support à moniteur (A-5009)", "Support à moniteur double. Peut supporter plusieurs tailles de moniteur.", 10, 50, 10),

((select Id from room where Number = 5003), "IKEA", "Burreau (A-5003)", "Burreau arrondi en bois de taille moyenne. Parfait pour des poste informatique.", 150, 200, 300),
((select Id from room where Number = 5003), "Herman Miller", "Chaise (A-5003)", "Chaise ergonomique avec roulette.", 50, 150, 50),
((select Id from room where Number = 5003), "Ergotron", "Support à moniteur (A-5003)", "Support à moniteur double. Peut supporter plusieurs tailles de moniteur.", 10, 50, 10),

((select Id from room where Number = 5002), "CCHIC", "Burreau (A-5002)", "Grand burreau en bois et en métal. Parfait pour des poste informatique.", 100, 200, 500),
((select Id from room where Number = 5002), "Herman Miller", "Chaise (A-5002)", "Chaise ergonomique avec roulette.", 50, 150, 50),
((select Id from room where Number = 5002), "Ergotron", "Support à moniteur (A-5002)", "Support à moniteur double. Peut supporter plusieurs tailles de moniteur.", 10, 50, 10),

((select Id from room where Number = 3007), "NoName", "Burreau (F-3007)", "Burreau moyen et peu chère. Utilisé pour les étudiants.", 100, 150, 300),
((select Id from room where Number = 3007), "FlexChair", "Chaise (F-3007)", "Chaise classique d'école en simli plastique et en métal.", 50, 100, 50),
((select Id from room where Number = 3007), "Unknown", "Burreau Prof (F-3007)", "Burreau pour les professeurs situé à l'avant de la classe.", 100, 250, 200),

((select Id from room where Number = 3009), "NoName", "Burreau (F-3009)", "Burreau moyen et peu chère. Utilisé pour les étudiants.", 100, 150, 300),
((select Id from room where Number = 3009), "FlexChair", "Chaise (F-3009)", "Chaise classique d'école en simli plastique et en métal.", 50, 100, 50),
((select Id from room where Number = 3009), "Unknown", "Burreau Prof (F-3009)", "Burreau pour les professeurs situé à l'avant de la classe.", 100, 250, 200),

((select Id from room where Number = 3011), "NoName", "Burreau (F-30011)", "Burreau moyen et peu chère. Utilisé pour les étudiants.", 100, 150, 300),
((select Id from room where Number = 3011), "FlexChair", "Chaise (F-3011)", "Chaise classique d'école en simli plastique et en métal.", 50, 100, 50),
((select Id from room where Number = 3011), "Unknown", "Burreau Prof (F-3011)", "Burreau pour les professeurs situé à l'avant de la classe.", 100, 250, 200);