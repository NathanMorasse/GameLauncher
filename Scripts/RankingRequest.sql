-- Get tous les scores d'un joueur selon un jeu
select `Id`, `User`, `Game`, `Category`, `Result`, `Time`, `Points`, `Date` 
from `scores` 
where `User` = 1 
and `Game` = (select `Id` from `games` where `Title` = "Sudoku");

-- Get tous les scores d'un joueur
select `scores`.`Id`, `users`.`Username` as `User`, `scores`.`Game`, `scores`.`Category`, `scores`.`Result`, `scores`.`Time`, `scores`.`Points`, `scores`.`Date` 
from `scores` 
join `users`
on `scores`.`User` = `users`.`Id` 
where `scores`.`User` = 2;

-- Classement des joueurs selon le nombre de points global
select  `scores`.`Id`, `users`.`Username` as `User`, `Game`, `Category`, `Result`, `Time`, Sum(`Points`) as `Points`, `Date`
from `scores`
join `users`
on `scores`.`User` = `users`.`Id` 
group by `User` 
order by Sum(`Points`) desc;

-- Get le joueur avec le plus de point
select  `scores`.`Id`, `users`.`Username` as `User`, `Game`, `Category`, `Result`, `Time`, Sum(`Points`) as `Points`, `Date`
from `scores`
join `users`
on `scores`.`User` = `users`.`Id` 
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
select  `scores`.`Id`, `users`.`Username` as `User`, `Game`, `Category`, `Result`, `Time`, Sum(`Points`) as `Points`, `Date`
from `scores`
join `users`
on `scores`.`User` = `users`.`Id` 
where `users`.`Department` = 1
group by `User`
order by Sum(`Points`) desc;

-- Get les meilleurs joueurs de chaque département pour un jeu
select  `scores`.`Id`, `users`.`Username` as `User`, `Game`, `Category`, `Result`, `Time`, Sum(`Points`) as `Points`, `Date`
from `scores`
join `users`
on `scores`.`User` = `users`.`Id` 
where `users`.`Department` = 1
and `Game` = (select `Id` from `games` where `Title` = "Sudoku")
group by `User`
order by Sum(`Points`) desc;

-- Get le classement des meilleurs départements par points Global
select 1 as `Id`, `departments`.`Department` as `User`, 1 as `Game`, null as `Category`, null as `Result`, null as `Time`, Sum(`Points`) as `Points`, null as `Date`
from `scores`
join `users`
on `scores`.`User` = `users`.`Id`
join `departments`
on `users`.`Department` = `departments`.`Id`
group by `departments`.`Department`
order by Sum(`Points`) desc;

-- Get le classement des meilleurs départements par points pour un jeu
select `departments`.`Department`, Sum(`Points`) as `Points`
from `scores`
join `users`
on `scores`.`User` = `users`.`Id`
join `departments`
on `users`.`Department` = `departments`.`Id`
where `Game` = (select `Id` from `games` where `Title` = "Sudoku")
group by `User`
order by Sum(`Points`) desc;




