-- Get tous les scores d'un joueur selon un jeu
select `Id`, `User`, `Game`, `Category`, `Result`, `Time`, `Points`, `Date` 
from `scores` 
where `User` = 1 
and `Game` = (select `Id` from `games` where `Title` = "Sudoku");

-- Get tous les scores d'un joueur
select `Id`, `User`, `Game`, `Category`, `Result`, `Time`, `Points`, `Date` 
from `scores` 
where `User` = 1;

-- Get le top trois des joueurs avec le plus de point
select  `Id`, `User`, `Game`, `Category`, `Result`, `Time`, `Points`, `Date` 
from `scores` 
group by `User` 
order by Sum(`Points`) desc 
limit 3;

-- Get le joueur avec le plus de point
select  `Id`, `User`, `Game`, `Category`, `Result`, `Time`, `Points`, `Date` 
from `scores` 
group by `User` 
order by Sum(`Points`) desc 
limit 1;

-- Get le meilleur temp d'un joueur pour le Sudoku
select `Time` 
from `scores` 
where `User` = 1 
and `Game` = (select `Id` from `games` where `Title` = "Sudoku") 
order by `Time`
limit 1;

-- Get le record de temp du sudoku
select `Time` 
from `scores` 
where `Game` = (select `Id` from `games` where `Title` = "Sudoku") 
order by `Time`
limit 1;

-- Get le meilleur temps du daily actuel
select `Time` 
from `scores` 
where `Category` = "Daily" 
and Date(`Date`) = curdate()
and `Game` = (select `Id` from `games` where `Title` = "Sudoku") 
order by `Time`
limit 1;

-- Get les meilleurs joueurs de chaque département

-- Get les meilleurs joueurs de chaque département pour un jeu

-- Get le top 3 des départements