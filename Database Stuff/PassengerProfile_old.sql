IF EXISTS(SELECT 1 FROM sys.tables WHERE object_id = OBJECT_ID('PassengerProfile'))
BEGIN;
    DROP TABLE [PassengerProfile];
END;
GO

CREATE TABLE [PassengerProfile] (
    [PassengerProfileID] INTEGER NOT NULL IDENTITY(1, 1),
    [FirstName] VARCHAR(255) NOT NULL,
    [LastName] VARCHAR(255) NOT NULL,
    [Email] VARCHAR(255) NOT NULL,
    [PhoneNumber] VARCHAR(15) NULL,
    PRIMARY KEY ([PassengerProfileID])
);
GO

INSERT INTO [PassengerProfile] (FirstName,LastName,Email,PhoneNumber)
VALUES
  ('Vaughan','Wheeler','v-wheeler@google.edu','(328) 159-1828'),
  ('Zelda','Kirkland','z_kirkland@protonmail.net','(221) 215-2955'),
  ('Oliver','Carey','oliver.carey@outlook.org','(337) 173-2414'),
  ('Curran','Zamora','curran_zamora3713@aol.edu','(786) 105-3734'),
  ('Dana','Mccormick','mccormick_dana396@hotmail.org','(575) 446-3431'),
  ('Eagan','Thompson','thompson.eagan3550@protonmail.org','(935) 277-0342'),
  ('Clementine','Talley','t.clementine@google.org','(132) 202-5573'),
  ('Chaney','Kim','chaney_kim@hotmail.edu','(757) 950-3153'),
  ('Curran','Phillips','curranphillips7694@icloud.org','(246) 868-5904'),
  ('Jason','Frederick','fjason@protonmail.net','(213) 720-4572'),
  ('Conan','Wilkinson','w-conan7941@google.net','(967) 471-3508'),
  ('Halee','Mann','h-mann@yahoo.edu','(754) 115-2672'),
  ('Keith','Anthony','keith-anthony@icloud.edu','(269) 517-8087'),
  ('Lael','Norman','lael_norman1361@yahoo.net','(452) 539-6837'),
  ('Carl','Mckinney','mckinney_carl@icloud.org','(168) 555-7386'),
  ('Deacon','Stein','stein_deacon4671@protonmail.edu','(712) 756-7344'),
  ('Phyllis','Gonzales','p_gonzales9400@protonmail.org','(948) 321-3417'),
  ('Yasir','Wilcox','wilcox-yasir@aol.net','(855) 265-3243'),
  ('Jelani','Villarreal','villarreal.jelani2049@yahoo.net','(691) 511-4983'),
  ('Kaseem','Rush','k-rush4008@aol.org','(541) 122-1875'),
  ('Malik','Sanders','sandersmalik@hotmail.net','(739) 472-3042'),
  ('Jana','Hewitt','j.hewitt5273@outlook.net','(726) 579-3028'),
  ('Ferdinand','Rojas','rojas-ferdinand@yahoo.edu','(384) 381-6772'),
  ('Felicia','Wolfe','f_wolfe@protonmail.net','(654) 845-8491'),
  ('Wayne','Mccormick','wmccormick@aol.net','(453) 516-0139'),
  ('Colton','Perkins','c-perkins@google.net','(361) 618-2221'),
  ('Hillary','Dalton','dalton-hillary@protonmail.edu','(173) 510-6650'),
  ('Oliver','Hutchinson','h.oliver@icloud.edu','(644) 853-5668'),
  ('Alden','Mejia','mejia.alden866@google.org','(912) 698-7226'),
  ('Ronan','Dorsey','dorsey-ronan4857@outlook.edu','(387) 508-1185'),
  ('Jarrod','Leach','leachjarrod@hotmail.com','(111) 284-4563'),
  ('Martha','Sweeney','sweeney-martha@hotmail.net','(742) 614-8376'),
  ('Carolyn','Hickman','h.carolyn@yahoo.org','(884) 357-3467'),
  ('Raven','Foreman','f.raven@aol.com','(625) 504-0532'),
  ('Zeus','Manning','zeusmanning4654@hotmail.org','(311) 168-4783'),
  ('Jesse','Robles','jrobles@protonmail.com','(292) 656-6254'),
  ('Bertha','Gilliam','bgilliam8558@protonmail.edu','(824) 945-4724'),
  ('Gage','Moody','g.moody@yahoo.net','(545) 600-2671'),
  ('Ruth','Joyner','jruth99@yahoo.net','(866) 568-4968'),
  ('Drew','Trujillo','drew_trujillo2363@google.com','(764) 555-4461'),
  ('Lee','Bryan','l-bryan@icloud.edu','(274) 789-0461'),
  ('Scott','Suarez','suarez_scott234@yahoo.edu','(277) 827-4387'),
  ('Hunter','Harper','h_harper@icloud.org','(719) 716-2193'),
  ('Jermaine','Kelley','kelley-jermaine@protonmail.org','(746) 882-7565'),
  ('Griffin','Hudson','g-hudson@outlook.net','(885) 565-8326'),
  ('Lesley','Ewing','e_lesley@protonmail.edu','(883) 325-2633'),
  ('Drake','Benton','bentondrake6160@outlook.net','(535) 134-5749'),
  ('Cade','Guerra','guerra-cade@outlook.edu','(254) 417-3854'),
  ('Colin','Castaneda','c.castaneda7568@hotmail.net','(362) 234-5770'),
  ('Madison','Vasquez','madison_vasquez5050@hotmail.net','(471) 835-3805');
