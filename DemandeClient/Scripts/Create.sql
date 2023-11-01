create table `Department` (
`Id` int not null auto_increment,
`Name` varchar(255) not null,
`Building` varchar(255),
`Floor` int,
primary key (Id)
);

create table `Room` (
`Id` int not null auto_increment,
`Department_Id` int not null,
`Number` int not null,
`HasAirConditionin` bool not null,
`HasHeaters` bool not null,
`HasPhone` bool not null,
`HasMovementSensor` bool not null,
primary key (Id),
foreign key (`Department_Id`) references `Department`(Id)
);

create table `ElectricOutlet` (
`Id` int not null auto_increment,
`Room_Id` int not null,
`Location` varchar(255),
primary key (Id),
foreign key (`Room_Id`) references `Room`(Id)
);

create table `LightSwitch` (
`Id` int not null auto_increment,
`Room_Id` int not null,
`Location` varchar(255),
primary key (Id),
foreign key (`Room_Id`) references `Room`(Id)
);

create table `Light` (
`Id` int not null auto_increment,
`Room_Id` int not null,
`LightSwitch_Id` int not null,
`Brand` varchar(255),
`Type` varchar(255),
primary key (Id),
foreign key (`Room_Id`) references `Room`(Id),
foreign key (`LightSwitch_Id`) references `LightSwitch`(Id)
);

create table `Board` (
`Id` int not null auto_increment,
`Room_Id` int not null,
`Type` varchar(255),
`Brand` varchar(255),
`Height` double,
`Width` double,
primary key (Id),
foreign key (`Room_Id`) references `Room`(Id)
);

create table `Tool` (
`Id` int not null auto_increment,
`Board_Id` int not null,
`Brand` varchar(255),
`Type` varchar(255),
primary key (Id),
foreign key (`Board_Id`) references `Board`(Id)
);

create table `NetworkEquipment` (
`Id` int not null auto_increment,
`Room_Id` int not null,
`Type` varchar(255),
`Brand` varchar(255),
`Model` varchar(255),
`Specifications` longtext,
primary key (Id),
foreign key (`Room_Id`) references `Room`(Id)
);

create table `Furniture` (
`Id` int not null auto_increment,
`Room_Id` int not null,
`Brand` varchar(255),
`Type` varchar(255),
`Description` varchar(255),
`Length` bool,
`Height` bool,
`Width` bool,
primary key (Id),
foreign key (`Room_Id`) references `Room`(Id)
);

create table `Period` (
`Id` int not null auto_increment,
`Room_Id` int not null,
`Start` time not null,
`End` time not null,
`Day` varchar(255) not null,
`TeacherName` varchar(255) not null,
`Class` varchar(255) not null,
`Group` varchar(255) not null,
primary key (Id),
foreign key (`Room_Id`) references `Room`(Id)
);

create table `Computer` (
`Id` int not null auto_increment,
`Room_Id` int not null,
`Brand` varchar(255),
`Model` varchar(255),
`OperatingSystem` varchar(255),
primary key (Id),
foreign key (`Room_Id`) references `Room`(Id)
);

create table `Display` (
`Id` int not null auto_increment,
`Room_Id` int not null,
`Computer_Id` int not null,
`Type` varchar(255),
`Brand` varchar(255),
`Model` varchar(255),
`Resolution` varchar(255),
primary key (Id),
foreign key (`Room_Id`) references `Room`(Id),
foreign key (`Computer_Id`) references `Computer`(Id)
);

create table `Peripheral` (
`Id` int not null auto_increment,
`Room_Id` int not null,
`Computer_Id` int not null,
`Brand` varchar(255),
`Model` varchar(255),
`Type` varchar(255),
`Description` varchar(255),
`IsWiFi` bool,
`IsBluetooth` bool,
primary key (Id),
foreign key (`Room_Id`) references `Room`(Id),
foreign key (`Computer_Id`) references `Computer`(Id)
);

create table `Software` (
`Id` int not null auto_increment,
`Brand` varchar(255),
`Name` varchar(255),
`Version` varchar(255),
`License` varchar(255),
`Compatibility` varchar(255),
`Architecture` varchar(255),
primary key (Id)
);

create table `ComputerSoftware` (
`Id` int not null auto_increment,
`Computer_Id` int not null,
`Software_Id` int not null,
primary key (Id),
foreign key (`Computer_Id`) references `Computer`(Id),
foreign key (`Software_Id`) references `Software`(Id)
);

create table `Case` (
`Id` int not null auto_increment,
`Computer_Id` int not null,
`Brand` varchar(255),
`Model` varchar(255),
`Compatibility` varchar(255),
`Height` double,
`Width` double,
`Length` double,
primary key (Id),
foreign key (`Computer_Id`) references `Computer`(Id)
);

create table `Fan` (
`Id` int not null auto_increment,
`Case_Id` int not null,
`Brand` varchar(255),
`Model` varchar(255),
`Size` varchar(255),
primary key (Id),
foreign key (`Case_Id`) references `Case`(Id)
);

create table `PowerSupply` (
`Id` int not null auto_increment,
`Case_Id` int not null,
`Brand` varchar(255),
`Model` varchar(255),
`Efficiency` varchar(255),
`IsModular` bool,
primary key (Id),
foreign key (`Case_Id`) references `Case`(Id)
);

create table `Motherboard` (
`Id` int not null auto_increment,
`Case_Id` int not null,
`Brand` varchar(255),
`Model` varchar(255),
`Compatibility` varchar(255),
`FormFactor` varchar(255),
primary key (Id),
foreign key (`Case_Id`) references `Case`(Id)
);

create table `GraphicCard` (
`Id` int not null auto_increment,
`Motherboard_Id` int not null,
`Brand` varchar(255),
`Model` varchar(255),
`Compatibility` varchar(255),
primary key (Id),
foreign key (`Motherboard_Id`) references `Motherboard`(Id)
);

create table `Processor` (
`Id` int not null auto_increment,
`Motherboard_Id` int not null,
`Brand` varchar(255),
`Model` varchar(255),
`Frequency` varchar(255),
primary key (Id),
foreign key (`Motherboard_Id`) references `Motherboard`(Id)
);

create table `DataStorage` (
`Id` int not null auto_increment,
`Motherboard_Id` int not null,
`Type` varchar(255),
`Brand` varchar(255),
`Model` varchar(255),
`Capacity` int,
`Speed` double,
primary key (Id),
foreign key (`Motherboard_Id`) references `Motherboard`(Id)
);

create table `RandomAccessMemory` (
`Id` int not null auto_increment,
`Motherboard_Id` int not null,
`Brand` varchar(255),
`Model` varchar(255),
`Capacity` int,
`Speed` int,
`Compatibility` varchar(255),
primary key (Id),
foreign key (`Motherboard_Id`) references `Motherboard`(Id)
);
