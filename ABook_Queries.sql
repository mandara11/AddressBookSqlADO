use ABook;

--UC1
create table ABookTable
	(
	FirstName varchar(20) not null,
	LastName varchar(20) not null,
	Address varchar(15) not null,
	City varchar(15) not null,
	State varchar(20) not null,
	Zipcode varchar(6) not null,
	PhoneNumber varchar(12) not null,
	Email varchar(30) not null
);
select * from ABookTable;

--UC2-3
USE ABook
GO

INSERT INTO ABookTable(FirstName,LastName,Address,City,State,Zipcode,PhoneNumber,Email)
     VALUES
           ('Mandara','Dammu','28/A','Bhilai','C.G.','490006','9674879893','monee99@gmail.com'),
		   ('Prudhvi','Devara','28/A','Hyd','T.L.','490566','8555466603','pru17@gmail.com'),
		   ('Rajat','Gupta','28/A','Vizag','A.P.','499876','7487009893','rajat002@gmail.com'),
		   ('Ankita','Dammu','LtWillaims','Raigarh','C.G.','346782','9683273393','ank675@gmail.com'),
		   ('Rishank','Daey','birsa/A','Bellary','K.A.','780006','989437893','rishu45@gmail.com'),
		   ('Chinmayi','Daey','Ban/A','Bhellary','K.A.','780006','924579893','chin56@gmail.com'),
		   ('Saras','Roy','Russian Comp','Bhilai','C.G.','490006','9609787863','sarasroy@gmail.com');
GO

select * from ABookTable;

--UC-4
use ABook;

update ABookTable set Address='Nehru Nagar'
where FirstName='Rishank';

select * from ABookTable;

--UC-5
use ABook;

delete from ABookTable 
where FirstName='Rishank';

select * from ABookTable;

--UC-6
use ABook;

select * from ABookTable
where City = 'Bhilai' and State = 'C.G.';

--UC7
use ABook;

select COUNT(City), City, State from ABookTable
group by State, City;

--UC-8
use ABook;

select * from ABookTable
where City = 'Bhilai'
order by FirstName asc;

--UC-9
use ABook;

alter table ABookTable
add RelationType varchar(15);


update ABookTable set RelationType ='Self' where FirstName='Mohanee';
update ABookTable set RelationType ='Friends' where FirstName='Prudhvi';
update ABookTable set RelationType ='Brother' where FirstName='Rajat';
update ABookTable set RelationType ='Cousin' where FirstName='Saras';
update ABookTable set RelationType ='Cousin' where FirstName='Ankita';
update ABookTable set RelationType ='Friends' where FirstName='Chinmayi';

select * from ABookTable;

--UC-9b
use Abook;
select * from ABookTable;

alter table ABookTable
drop column RelationType;

create table RelationTable
(
	FirstName varchar(25) not null,
	LastName varchar(25) not null,
	RelationType varchar(20) not null
);

insert into ABookTable values
 ('Shriya','Basak','Newtown','Kolkata','WB','711006','9672379893','shriyab@gmail.com'),
  ('Prateek','Shukla','LajpatNagar','Lucknow','UP','490234','9674567890','miket@gmail.com'),
   ('Kaushal','Kumar','Bisra','Patna','BH','211906','8362478920','kaushalk@gmail.com');

insert into RelationTable(FirstName,RelationType)
 values ('Mandara','Self'),
 ('Prudhvi','Friend'),
 ('Ankita','Family'),
 ('Chinmayi','Friend'),
 ('Saras','Family'),
 ('Rajat','Family'),
 ('Shriya','Professional'),
 ('Prateek','Professional'),
 ('Kaushal','Professional')
 ;

 select * from RelationTable;

 select * from ABookTable a inner join RelationTable r 
 on (a.FirstName = r.FirstName);

 --UC-10
 use ABook;

select Count(FirstName) as No_of_Contacts,RelationType from ABookTable group by RelationType;

--UC-11
use ABook;

insert into AbookTable values 
('Shriya','Basak','Newtown','Kolkata','WB','711006','9672379893','shriyab@gmail.com');

insert into RelationTable(FirstName,RelationType) values
('Prudhvi','Family');


 select * from ABookTable a inner join RelationTable r 
 on (a.FirstName = r.FirstName) order by a.FirstName;

 --UC12-13
 use ABook;

--Splitting the table into other tables
--Name, ContactInfo,Address,RelationType

alter table ABookTable
drop column PhoneNumber, Address, City, State, Email;
alter table ABookTable drop column ZipCode;

alter table ABookTable 
add unique (FirstName);

alter table ABookTable
add PersonID int identity(1,1) not null primary key;

alter table ABookTable drop column PersonID;

alter table ABookTable
add unique (FirstName);

alter table ABookTable
add primary key (FirstName);

select * from ABookTable;


--Cmd to check for Primary Keys in a table
SELECT *
FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE
WHERE OBJECTPROPERTY(OBJECT_ID(CONSTRAINT_SCHEMA + '.' + QUOTENAME(CONSTRAINT_NAME)), 'IsPrimaryKey') = 1
AND TABLE_NAME = 'ABookTable';


---Contact Info Table
create table ContactInfo  
(
	FirstName varchar(20) not null foreign key references ABookTable(FirstName),
	PhoneNumber varchar(13) not null,
	Email varchar(25) not null
);

alter table ContactInfo add foreign key (FirstName) references ABookTable(FirstName);

INSERT INTO ContactInfo(FirstName,PhoneNumber,Email)
     VALUES
           ('Mandara','9674879893','monee99@gmail.com'),
		   ('Prudhvi','8555466603','pru17@gmail.com'),
		   ('Rajat','7487009893','rajat002@gmail.com'),
		   ('Ankita','7487009893','ank675@gmail.com'),
		   ('Chinmayi','924579893','chin56@gmail.com'),
		   ('Saras','9609787863','sarasroy@gmail.com'),
		    ('Shriya','9672379893','shriyab@gmail.com'),
			('Prateek','9674567890','miket@gmail.com'),
			('Kaushal','8362478920','kaushalk@gmail.com');
GO

select * from ContactInfo;


--AddressInfo Table
create table AddressInfo
(
	FirstName varchar(20) not null foreign key references ABookTable(FirstName),
	Address varchar(15) not null,
	City varchar(15) not null,
	State varchar(20) not null,
	Zipcode varchar(6) not null,
);

INSERT INTO AddressInfo(FirstName,Address,City,State,Zipcode)
     VALUES
           ('Mandara','28/A','Bhilai','C.G.','490006'),
		   ('Prudhvi','28/A','Hyd','T.L.','490566'),
		   ('Rajat','28/A','Vizag','A.P.','499876'),
		   ('Ankita','LtWillaims','Raigarh','C.G.','579893'),
		   ('Chinmayi','Ban/A','Bhellary','K.A.','780006'),
		   ('Saras','Russian Comp','Bhilai','C.G.','490006'),
		    ('Shriya','Newtown','Kolkata','WB','711006'),
			('Prateek','LajpatNagar','Lucknow','UP','490234'),
			('Kaushal','Bisra','Patna','BH','211906');
GO

select * from AddressInfo;

select a.FirstName,i.City from ABookTable a join AddressInfo i 
on (a.FirstName= i.FirstName) where i.City='Bhilai'
order by a.FirstName;

select n.FirstName, n.LastName, af.Address, af.City, af.State, af.Zipcode, cf.PhoneNumber,cf.Email, rt.RelationType
from AddressInfo af join ABookTable n on  af.FirstName= n.FirstName
join ContactInfo cf on cf.FirstName=af.FirstName
join RelationTable rt on af.FirstName=rt.FirstName;