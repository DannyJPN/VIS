alter table [GroupLeading] drop constraint leadingperson_fk   
go
alter table [GroupLeading] drop constraint leadedgroup_fk     
go
alter table [Address] drop constraint citypart_fk        		 
go
alter table [Address] drop constraint city_fk            		 
go
alter table [CityPart] drop constraint citypart_tocity_fk 		 
go
alter table [Child] drop constraint child_mother_fk    			 
go
alter table [Child] drop constraint child_father_fk 				 
go
alter table [Child] drop constraint address_fk 					 
go
alter table [Child] drop constraint insurance_fk 					 
go
alter table [GroupMembership] drop constraint child_group_fk 	
go
alter table [GroupMembership] drop constraint visited_group_fk 	 
go
alter table [Parent] drop constraint parent_pk                     
go
alter table [HobbyGroup] drop constraint hbgroup_pk         		 
go
alter table [City] drop constraint city_pk            			 
go
alter table [CityPart] drop constraint citypart_pk        		 
go


alter table [Child] drop constraint child_pk           			 
go

alter table [GroupLeader] drop constraint gleader_pk 				 
go
alter table [Address] drop constraint homeadd_pk 				 
go
alter table [InsuranceCompany] drop constraint inscomp_pk 				 
go



drop table [GroupMembership]
go
drop table [Child]
go
drop table [Address]
go
drop table [CityPart]
go
drop table [City]
go
drop table [GroupLeading]
go
drop table [HobbyGroup]
go
drop table [GroupLeader]
go
drop table [Parent]
go
drop table [InsuranceCompany]
go









