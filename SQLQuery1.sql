create procedure SpAddContactDetails
(
	@FirstName varchar(20),
	@LastName varchar(20),
	@DateAdded datetime,
	@Address  VARCHAR (15),
    @City    VARCHAR (15),
    @State    VARCHAR (20), 
    @Zipcode   VARCHAR (6),
	@PhoneNumber VARCHAR (13) ,
    @Email     VARCHAR (25),
	@RelationType VARCHAR (20)
)
as
begin 

insert into ABookTable values (@FirstName,@LastName,@DateAdded);
insert into AddressInfo values (@FirstName,@Address,@City,@State,@Zipcode);
insert into ContactInfo values (@FirstName,@PhoneNumber,@Email);
insert into RelationTable values (@FirstName,@RelationType);

end