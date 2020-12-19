USE [aspnet-EstadiasUTTN-20201126063537] /*remplazar por nombre de bd*/
GO
/****** Object:  Table [dbo].[__MigrationHistory]    Script Date: 18/12/2020 09:47:48 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__MigrationHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ContextKey] [nvarchar](300) NOT NULL,
	[Model] [varbinary](max) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK_dbo.__MigrationHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC,
	[ContextKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 18/12/2020 09:47:48 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](128) NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 18/12/2020 09:47:48 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 18/12/2020 09:47:48 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](128) NOT NULL,
	[ProviderKey] [nvarchar](128) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC,
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 18/12/2020 09:47:48 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](128) NOT NULL,
	[RoleId] [nvarchar](128) NOT NULL,
	[xd] [nvarchar](50) NULL,
 CONSTRAINT [PK_dbo.AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 18/12/2020 09:47:48 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](128) NOT NULL,
	[Email] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEndDateUtc] [datetime] NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[UserName] [nvarchar](256) NOT NULL,
	[Nombre] [nvarchar](50) NULL,
	[Apellido] [nvarchar](50) NULL,
 CONSTRAINT [PK_dbo.AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Directorio]    Script Date: 18/12/2020 09:47:48 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Directorio](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](max) NULL,
	[Cargo] [nvarchar](150) NULL,
	[Extension] [int] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Organigrama]    Script Date: 18/12/2020 09:47:48 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Organigrama](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[title] [nvarchar](50) NULL,
	[name] [nvarchar](max) NULL,
	[datetime] [datetime] NULL,
	[idusuario] [nvarchar](128) NULL,
 CONSTRAINT [PK_Organigrama] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[__MigrationHistory] ([MigrationId], [ContextKey], [Model], [ProductVersion]) VALUES (N'202011270102176_InitialCreate', N'EstadiasUTTN.Models.ApplicationDbContext', 0x1F8B0800000000000400DD5CDB6EE436127D0FB0FF20E86937705ABEEC0C26467702A76DEF1A3BBE60BA1DECDB802DB1DBC2489422511E1B41BE2C0FF9A4FC424889BAF02651DDEA8B8300819B2C9E2A16ABC862A9387FFEFEC7F8C79730B09E6192FA119AD827A363DB82C88D3C1FAD26768697DF7DB07FFCE11FDF8CAFBCF0C5FAB9A43BA37464244A27F613C6F1B9E3A4EE130C413A0A7D3789D26889476E143AC08B9CD3E3E3EF9D93130712089B6059D6F85386B01FC2FC07F9398D900B639C81E036F26090B276D233CB51AD3B10C234062E9CD85729069E0FD2C7F9FC6E5490DBD645405A26F60C064BDB020845186022E8F9630A673889D06A16930610CC5F6348E8962048219BC0794D6E3A97E3533A17A71E5842B9598AA3B027E0C919538E230E5F4BC576A53CA2BE2BA266FC4A679DAB7062DF78306FFA1405440122C3F3699050E2897D5BB1B848E33B8847E5C05101799D10B8AF51F265D4443CB28CC71D55C6743A3AA6FF1D59D32CC059022708663801C191F5902D02DFFD1F7C9D475F209A9C9D2C96671FDEBD07DED9FB7FC3B377CD9992B9123AAE81343D24510C13221B5C56F3B72D871FE78803AB618D318556882D11BFB0AD5BF0F211A2157E221E73FAC1B6AEFD17E8952DCCB81E914FDC880CC249467EDE6541001601ACFA9D569EF4FF2D5C4FDFBD1F84EB1D78F657F9D20BFC89E324C4AF3EC120EF4D9FFCB8702F6EBD3F33B2EB240AE96FDEBE8ADECFB3284B5C3A99484B3207C90A625EBAB1531BAF914953A8E1CDBA443D7CD3A692CAE6AD24A5135AC7134A16BBF68652DEEDF235B6B88B38268B979B16D5489BC1294EAB9130FCC86A12D5C673626A3C884CEA6FBD1746E12269DB0DC99F467CDBD95CC430087C2FDA3AA3AB10F8C1009BBB011712582DFD2484D5AAFD14115702A8F71A3C8034257B9BF75F903E6D5D4133E8660971B9190661BC756E0F4F11827759B8A09EBC3B5E832DCDFC6B740D5C1C2557888EDA18EF63E47E89327C85BC4B80E123764B40FA73EE87E600838873E1BA304DAF8931436F1A917B43097883F0D9696F38BAE3EE3BB89A06C00FD5D19570367C2E49EB084B4D2145591A3255A4D526EAC768E52333514B52BDA80545A7A88CACAFA814CC4C5246A9173427E894B3A01A2C76CD5768F8E035873DFCE875B36044B71734D438233B24FC0F443021DB98F700308609AA57C064DFD847F0932F1F65BAF5B329E7F43308B2A159ADE50DF92630BC37E4B087EF0DB998A4F9D9F768546270A52B8909BC11BDFAB6D8ED738264BB76076E9ABB66BE9B3D40E72E17691AB97EEE058A641E4BC5F0F29318CEEACECB14B311733B6462C4D07D7AE4911632375B34AA7B74090388A175E116C9CE29485DE0C96A2413F27A08569EA80AC1EA1C0F2FDCB7124F62E930A18300BD04A5C4537D8465B7F091EBC720E8D49230D2F008A373AF78883D9730868832ECD4840973754A870A50F11116A54B4363A76171ED86A8895A756BDE15C2D6EB2E655A7662931DB1B3C62E59FCB615C36CD7D80E8CB35D25260268D393FB30507657313500F1E27268062ADC983406CA42AA9D1828AFB13D1828AF923767A0C515D574FD85FBEAA199277F51DEFDB1DEAAAE3DD826A78F0333CD22F6246330190113D93C2F17B413BE60C5E58CC8C9EE67290B754513A1E03388F9944D1DEF2AE350A71D4434A236C0DAD03A40D9874D094872A81EC295B9BC56E95814D103B6CCBBB5C2B2BD5F806DD8808CDDFCC0DB20D47F06168DD3E8F651CDACB206C9C88D2E0B0D1C8541889B173F7103A5E8F2B2B2624C62E13ED17063626C315A14D411B96A94544E66702D95A6D9AD255540D62724DB484B42F8A4D1523999C1B5C46CB45B498AA0A04758B0918AF8237C20672B331DD56953F58D9DA2F08B358C1D4D85D8F816C4B18F568D8A31D662CD8A72B1E977B3FE65546181E1B8A9A29AAA92B6E284A304ACA0D04B581349AFFD24C597008305A0799EA9174A64CAB355B3FD972C9BC7A7BC88E5395052D3BFD9476145390277D8CAD10803B926530C694893E7D11506A01E6ED1123E10804491BA9F464116227D84A51F5D7CC06B8E2F5A6484B123C82F455092BAA43897D7BDD1CAC85E31D42A5511CCFA2BA587D0E9BB8C3F9B1AD7C5A47A943245D544D1A5ADF6B672BA50A6DF6A896162FFC5EA44D89267B15A1BCEB7589B394A5D4AD3C4A95BCD9158AD4C138635F5C468945B48608D3E7354BE22A689C9F798230A652F4D48A1AB8794CDE2164EC866C75A781A8DAA29CC39C8E52C4D74B9D71C5951D8D2845674AF81AD9059ECEBE14772ED0BE75072B739765D0823EEEA077C966A2F53EB1FA6C5857BB3D35483B19D2D7A98C3B85157D0046A34F7C462950312186B3F4873D2DE3AD737A722D1B299396930F47B0FF7499EDF7A5AEB08F498DC77766E7B6FAB33D0E3F533DAAD9A8674EB14492AEED5ED53B8658ED98DAFFBB19274052C486CAB542339DA5F530CC3112518CD7E09A6810FE9465E12DC02E42F618A8BDA12FBF4F8E45478EE74384F8F9C34F502C58D59F7FE885FB31D9489A16790B84F20918B3636789E53834AF9F01BE4C19789FD6B3EEA3C4FADD0BFF2E623EB267D44FE2F19E9982719B47E938B508779AE60F040A612F4B737F1F2C45CE537FFFF5C0C3DB2EE13E24EE7D6B1A0E875969F7F8FD24B9A62E806D2ACFD4AE50D7B1BF700A484FD67085EFED5C45AE791C74660DC430EE56C052F5EFFDDC6C2C783BCD9D868BECA77191B212ADE5E0C8537880A756F2BD6C1D2BEABF0C84F9CBFABE83759F53B8B7544D3BEB1F0517F30F18585F9F6588EDCE3F9A8B8C9ED62ABCCF5DC59A1BE51B9EABECF4CA9907D2347978BD57BC00D5A90BE59E8F4C60ABD073BD21575DC8361EFD3EEB75EBC7D28F5DAF56562BF65DABBACCC6EF9F6F6B72AC83E8012424549D4FECBAE776D6BBAD4F481D7AEF62BAE3E306363C7FCFE4BA8776D6CBAC4F5811B5BAF42E903B3B57D9D9F7BB634E32374EF65CF720597E613932ABBDD55D65C7C0A20D7FF05CD46151165F11A555D47D75603DCC1B026D133D517F0898C25C791F84A14ED6CFBCD951DF8AD936534ED6C3565AF6DBCD9FEDFCA9BD1B4F3D61493EEA3205B59CEA92A92EFD8C7DA2ACDDE52013637938E7AFFAE98B5B55EE02DD55B0FA214CE7B345FBDDF4E79F5202A19D2757A9453CB1FB0C9D9D9F83739C9F99DFAAB1A82FE0B9D08BADCA959D1DCA065541EDE8244258990A1B9851878E448BD48B0BF042E26DD34019D3FA7CF937AF433C8027A37E83EC37186C99461B808B884170D02DAF8E735E3BCCCE3FB98FE4A87980211D3A789FB7BF453E6075E25F7B52227A481A0D1054BF7D2B5C434EDBB7AAD90EE226408C4D45705457318C601014BEFD10C3CC3756423E6F711AE80FB5A67007520DD0BC1AB7D7CE9835502C29461D4E3C94F62C35EF8F2C35FF093E1979A560000, N'6.2.0-61023')
GO
INSERT [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'1', N'Administrador')
INSERT [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'2', N'Usuario')
GO
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId]
GO
/****** Object:  StoredProcedure [dbo].[PA_UserDeleteRol]    Script Date: 18/12/2020 09:47:48 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[PA_UserDeleteRol]
	-- Add the parameters for the stored procedure here
		@UserId nvarchar(50)
AS
BEGIN
	
		--Elimina un usuario de su rol.

			DELETE FROM AspNetUserRoles WHERE UserId = @UserId

END
GO
/****** Object:  StoredProcedure [dbo].[PA_UserRolesInsert]    Script Date: 18/12/2020 09:47:48 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[PA_UserRolesInsert]
	/*
	@UserName = Usuario que recibira un rol
	@Rol = Id del rol que sera asignado.
					(1 = Administrador)
					(2 = Tecnico)
					(3 = Usuario)
	*/
	 @IdUserName nvarchar(50),
	 @Rol int
AS
BEGIN
	
		--Inserta un usuario al rol (RoleId) valores 1 = Admin, 2 = Tecnico, 3 = Usuario.
			/*DECLARE @id nvarchar(128)
			SELECT @id=id
			FROM AspNetUsers
			WHERE UserName = @UserName*/

			INSERT INTO AspNetUserRoles
			([UserId],[RoleId])
			VALUES (@IdUserName, @Rol)

END
GO
/****** Object:  StoredProcedure [dbo].[PA_UserRolesUpdate]    Script Date: 18/12/2020 09:47:48 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[PA_UserRolesUpdate]
	@RoleId int,
	@UserId nvarchar(128)
AS
BEGIN
			UPDATE AspNetUserRoles
			SET RoleId = @RoleId
			WHERE UserId = @UserId
END
GO
USE [master]
GO
ALTER DATABASE [aspnet-EstadiasUTTN-20201126063537] SET  READ_WRITE 
GO
