create table `Departments` (
`Id` int not null auto_increment,
`Department` varchar(255) not null,
primary key (Id)
);
    
create table `Users` (
`Id` int not null auto_increment,
`Username` varchar(255) not null,
`Password` varchar(255) not null,
`Department` int not null,
primary key (Id),
foreign key (Department) references Departments(Id)
);

create table `Games` (
`Id` int not null auto_increment,
`Title` varchar(255) not null,
primary key (Id)
);

create table `Scores` (
`Id` int not null auto_increment,
`User` int not null,
`Game` int not null,
`Category` varchar(255),
`Result` varchar(255),
`Time` time,
`Points` int,
`Date` datetime,
primary key (Id),
foreign key (`User`) references Users(Id),
foreign key (`Game`) references Games(Id)
);

create table `Saves` (
`Id` int not null auto_increment,
`User` int not null,
`Game` int not null,
`Save` longtext not null,
`Slot` varchar(255) not null,
`Time` time not null,
`Date` datetime not null,
primary key (Id),
foreign key (`User`) references Users(Id),
foreign key (`Game`) references Games(Id)
);