/****** Object:  Database [eLiDAR]    Script Date: 2022-05-30 6:43:43 PM ******/
CREATE DATABASE [eLiDAR]  (EDITION = 'Basic', SERVICE_OBJECTIVE = 'Basic', MAXSIZE = 2 GB) WITH CATALOG_COLLATION = SQL_Latin1_General_CP1_CI_AS;
GO
ALTER DATABASE [eLiDAR] SET COMPATIBILITY_LEVEL = 150
GO
ALTER DATABASE [eLiDAR] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [eLiDAR] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [eLiDAR] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [eLiDAR] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [eLiDAR] SET ARITHABORT OFF 
GO
ALTER DATABASE [eLiDAR] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [eLiDAR] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [eLiDAR] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [eLiDAR] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [eLiDAR] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [eLiDAR] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [eLiDAR] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [eLiDAR] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [eLiDAR] SET ALLOW_SNAPSHOT_ISOLATION ON 
GO
ALTER DATABASE [eLiDAR] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [eLiDAR] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [eLiDAR] SET  MULTI_USER 
GO
ALTER DATABASE [eLiDAR] SET ENCRYPTION ON
GO
ALTER DATABASE [eLiDAR] SET QUERY_STORE = ON
GO
ALTER DATABASE [eLiDAR] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 100, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
/*** The scripts of database scoped configurations in Azure should be executed inside the target database connection. ***/
GO
-- ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 8;
GO
/****** Object:  User [datacollector]    Script Date: 2022-05-30 6:43:43 PM ******/
CREATE USER [datacollector] FOR LOGIN [datacollector] WITH DEFAULT_SCHEMA=[dbo]
GO
sys.sp_addrolemember @rolename = N'db_datareader', @membername = N'datacollector'
GO
sys.sp_addrolemember @rolename = N'db_datawriter', @membername = N'datacollector'
GO
/****** Object:  UserDefinedFunction [dbo].[GetIsComplete]    Script Date: 2022-05-30 6:43:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:      <Author, , Name>
-- Create Date: <Create Date, , >
-- Description: <Description, , >
-- =============================================
CREATE FUNCTION [dbo].[GetIsComplete]
(
    -- Add the parameters for the function here
    @plotid nvarchar(50)
)
RETURNS bit
AS
BEGIN
    -- Declare the return variable here
    DECLARE @ResultVar bit

	SET @Resultvar = 0
    -- Add the T-SQL statements to compute the return value here
    SELECT @ResultVar = PLOT.IsComplete from PLOT where PLOTID = @plotid;
	RETURN @ResultVar
    -- Return the result of the function
--    RETURN @ResultVar
END
GO
/****** Object:  UserDefinedFunction [dbo].[IsPlotCompletefromPlotID]    Script Date: 2022-05-30 6:43:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:      <Author, , Name>
-- Create Date: <Create Date, , >
-- Description: <Description, , >
-- =============================================
CREATE FUNCTION [dbo].[IsPlotCompletefromPlotID]
(
    -- Add the parameters for the function here
    @plotid nvarchar(50)
)
RETURNS bit
AS
BEGIN
    -- Declare the return variable here
    DECLARE @ResultVar bit

	SET @Resultvar = 0
    -- Add the T-SQL statements to compute the return value here
    SELECT @ResultVar = 
	    CASE WHEN s.Contractor_Complete < CAST(CURRENT_TIMESTAMP as datetime2) THEN 1
        ELSE 0
		END
		from STATUS s where s.PLOTID = @plotid;
	RETURN @ResultVar
    -- Return the result of the function
--    RETURN @ResultVar
END
GO
/****** Object:  UserDefinedFunction [dbo].[IsPlotCompletefromTreeID]    Script Date: 2022-05-30 6:43:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:      <Author, , Name>
-- Create Date: <Create Date, , >
-- Description: <Description, , >
-- =============================================
CREATE FUNCTION [dbo].[IsPlotCompletefromTreeID]
(
    -- Add the parameters for the function here
    @treeid nvarchar(50)
)
RETURNS bit
AS
BEGIN
    -- Declare the return variable here
    DECLARE @ResultVar bit

	SET @Resultvar = 0
    -- Add the T-SQL statements to compute the return value here
    SELECT @ResultVar = 
	    CASE WHEN s.Contractor_Complete < CAST(CURRENT_TIMESTAMP as datetime2) THEN 1
        ELSE 0
		END
		from STATUS s where s.PLOTID = (Select PLOTID from dbo.TREE where TREEID =  @treeid);
	RETURN @ResultVar
    -- Return the result of the function
--    RETURN @ResultVar
END
GO
/****** Object:  Table [dbo].[ECOSITE]    Script Date: 2022-05-30 6:43:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ECOSITE](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ECOSITEID] [nvarchar](50) NULL,
	[PLOTID] [nvarchar](50) NULL,
	[HUMUSFORMCODE] [int] NULL,
	[DRAINAGECLASSCODE] [int] NULL,
	[STRATIFIED] [nvarchar](10) NULL,
	[EFFECTIVE_PORE_PATTERN] [nvarchar](10) NULL,
	[ELC_SUBSTRATE_TYPE] [nvarchar](10) NULL,
	[DEPTHTODISTINCTMOTTLES] [int] NULL,
	[DEPTHTOPROMINENTMOTTLES] [int] NULL,
	[DEPTHTOGLEY] [int] NULL,
	[DEPTHTOBEDROCK] [int] NULL,
	[DEPTHTOCARBONATES] [int] NULL,
	[MOISTURE_REGIME_DEPTH_CLASS] [nvarchar](10) NULL,
	[MOISTUREREGIMECODE] [nvarchar](10) NULL,
	[MODEOFDEPOSITIONCODE1] [nvarchar](10) NULL,
	[MODEOFDEPOSITIONCODE2] [nvarchar](10) NULL,
	[FUNCTIONALROOTINGDEPTH] [int] NULL,
	[MAXIMUMROOTINGDEPTH] [int] NULL,
	[DEPTHTOROOTRESTRICTION] [int] NULL,
	[DEPTHTOWATERTABLE] [int] NULL,
	[DEPTHTOIMPASSABLECOARSEFRAGMENTS] [int] NULL,
	[PROBLEMATICSITE] [nvarchar](10) NULL,
	[DEPTHTOSEEPAGE] [int] NULL,
	[SOIL_PIT_PHOTO] [nvarchar](10) NULL,
	[PRI_ECO] [nvarchar](50) NULL,
	[PRI_ECO_PCT] [int] NULL,
	[SEC_ECO] [nvarchar](50) NULL,
	[SEC_ECO_PCT] [int] NULL,
	[AZIMUTH] [int] NULL,
	[DISTANCE] [int] NULL,
	[SOIL_PIT_PHOTO1] [varbinary](1) NULL,
	[SOIL_PIT_PHOTO2] [varbinary](1) NULL,
	[SUBSTRATENOTE] [text] NULL,
	[POREPATTERNCODE] [int] NULL,
	[MINERALTEXTURECODE] [nvarchar](50) NULL,
	[DECOMPOSITIONCODE] [nvarchar](10) NULL,
	[PITAZIMUTH] [int] NULL,
	[PITDISTANCE] [float] NULL,
	[SUBSTRATEDATE] [nvarchar](50) NULL,
	[SUBSTRATEPERSON] [nvarchar](100) NULL,
	[Created] [datetime2](7) NULL,
	[LastModified] [datetime2](7) NULL,
	[IsDeleted] [nvarchar](1) NULL,
	[CreatedAtServer] [datetime2](7) NULL,
	[LastModifiedAtServer] [datetime2](7) NULL,
	[ERRORCOUNT] [int] NULL,
	[ERRORMSG] [nvarchar](max) NULL,
 CONSTRAINT [PK__ECOSITE__3214EC27BB060CB3] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UC_ECOSITEID] UNIQUE NONCLUSTERED 
(
	[ECOSITEID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PHOTO]    Script Date: 2022-05-30 6:43:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PHOTO](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PHOTOID] [nvarchar](50) NULL,
	[PLOTID] [nvarchar](50) NULL,
	[PHOTOTYPE] [nvarchar](50) NULL,
	[DESCRIPTION] [text] NULL,
	[PHOTONUMBER] [int] NULL,
	[FRAMENUMBER] [int] NULL,
	[AZIMUTH] [int] NULL,
	[DISTANCE] [float] NULL,
	[Created] [datetime2](7) NULL,
	[LastModified] [datetime2](7) NULL,
	[IsDeleted] [nvarchar](1) NULL,
	[CreatedAtServer] [datetime2](7) NULL,
	[LastModifiedAtServer] [datetime2](7) NULL,
	[ERRORCOUNT] [int] NULL,
	[ERRORMSG] [nvarchar](max) NULL,
 CONSTRAINT [PK__PHOTO__3214EC27F923FD56] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UC_PHOTOID] UNIQUE NONCLUSTERED 
(
	[PHOTOID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PLOT]    Script Date: 2022-05-30 6:43:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PLOT](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PLOTID] [nvarchar](50) NULL,
	[PROJECTID] [nvarchar](50) NULL,
	[VSNPLOTTYPECODE] [nvarchar](10) NULL,
	[VSNPLOTNAME] [nvarchar](20) NULL,
	[PLOTKEY] [text] NULL,
	[PLOTOVERVIEWDATE] [nvarchar](50) NULL,
	[MEASURETYPECODE] [text] NULL,
	[LEAD_SPP] [int] NULL,
	[GROWTHPLOTNUMBER] [int] NULL,
	[CROWN_CLOSURE] [int] NULL,
	[FIELD_CREW1] [nvarchar](100) NULL,
	[FIELD_CREW2] [nvarchar](100) NULL,
	[FIELD_CREW3] [nvarchar](100) NULL,
	[FIELD_CREW4] [nvarchar](100) NULL,
	[FIELD_CREW5] [nvarchar](100) NULL,
	[FIELD_CREW6] [nvarchar](100) NULL,
	[UTMZONE] [int] NULL,
	[EASTING] [float] NULL,
	[NORTHING] [float] NULL,
	[DATUM] [nvarchar](50) NULL,
	[PLOTOVERVIEWNOTE] [text] NULL,
	[NONSTANDARDTYPECODE] [int] NULL,
	[ACCESSCONDITIONCODE] [int] NULL,
	[MEASUREYEAR] [int] NULL,
	[EXISTINGPLOTNAME] [nvarchar](50) NULL,
	[EXISTINGPLOTTYPECODE] [nvarchar](50) NULL,
	[DISTANCETARGETMOVED] [float] NULL,
	[AZIMUTHTARGETMOVED] [int] NULL,
	[MATURITYCLASSCODE1] [nvarchar](50) NULL,
	[MATURITYCLASSRATIONALE1] [nvarchar](max) NULL,
	[MATURITYCLASSCODE2] [nvarchar](50) NULL,
	[MATURITYCLASSRATIONALE2] [nvarchar](max) NULL,
	[MAINCANOPYORIGINCODE1] [int] NULL,
	[MAINCANOPYORIGINCODE2] [int] NULL,
	[DISTURBANCECODE1] [int] NULL,
	[DISTURBANCECODE2] [int] NULL,
	[SITERANK] [int] NULL,
	[CROWNDAMAGECODE] [int] NULL,
	[VIGOURCODE] [int] NULL,
	[DAMAGEDESCRIPTION] [text] NULL,
	[FORESTHEALTHDATE] [nvarchar](50) NULL,
	[FORESTHEALTHNOTE] [text] NULL,
	[FORESTHEALTHPERSON] [nvarchar](100) NULL,
	[FOLLOWUPREQUIRED] [nvarchar](1) NULL,
	[SMALLTREESHRUBAREA] [int] NULL,
	[SMALLTREESHRUBDATE] [nvarchar](50) NULL,
	[SMALLTREESHRUBNOTE] [text] NULL,
	[SMALLTREEPERSON] [nvarchar](100) NULL,
	[UNDERSTORYVEGETATIONDATE] [nvarchar](50) NULL,
	[UNDERSTORYVEGETATIONAREA] [int] NULL,
	[UNDERSTORYVEGETATIONNOTE] [text] NULL,
	[UNDERSTORYVEGETATIONPERSON] [nvarchar](100) NULL,
	[UNDERSTORYCENSUSDATE] [nvarchar](50) NULL,
	[UNDERSTORYCENSUSNOTE] [text] NULL,
	[UNDERSTORYCENSUSPERSON] [nvarchar](100) NULL,
	[DOWNWOODYDEBRISDATE] [nvarchar](50) NULL,
	[DOWNWOODYDEBRISNOTE] [text] NULL,
	[DOWNWOODYDEBRISPERSON] [nvarchar](100) NULL,
	[DEFORMITYDATE] [nvarchar](50) NULL,
	[DEFORMITYNOTE] [text] NULL,
	[DEFORMITYPERSON] [nvarchar](100) NULL,
	[STANDINFODATE] [nvarchar](50) NULL,
	[STANDINFONOTE] [text] NULL,
	[STANDINFOPERSON] [nvarchar](100) NULL,
	[PERCENTAFFECTED] [int] NULL,
	[PERCENTMORTALITY] [int] NULL,
	[Created] [datetime2](7) NULL,
	[LastModified] [datetime2](7) NULL,
	[IsDeleted] [nvarchar](1) NULL,
	[CreatedAtServer] [datetime2](7) NULL,
	[LastModifiedAtServer] [datetime2](7) NULL,
	[DECLINATION] [int] NULL,
	[CANOPYSTRUCTURECODE1] [nvarchar](10) NULL,
	[CANOPYSTRUCTURECODE2] [nvarchar](10) NULL,
	[LINELENGTH1] [float] NULL,
	[LINELENGTH2] [float] NULL,
	[AGEDATE] [nvarchar](50) NULL,
	[AGENOTE] [text] NULL,
	[AGEPERSON] [nvarchar](100) NULL,
	[STEMMAPPINGDATE] [nvarchar](50) NULL,
	[STEMMAPPINGNOTE] [text] NULL,
	[STEMMAPPINGPERSON] [nvarchar](50) NULL,
	[IsComplete] [bit] NULL,
	[ERRORCOUNT] [int] NULL,
	[ERRORMSG] [nvarchar](max) NULL,
 CONSTRAINT [PK__PLOT__3214EC27489FC0CD] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UC_PLOTID] UNIQUE NONCLUSTERED 
(
	[PLOTID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SMALLTREE]    Script Date: 2022-05-30 6:43:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SMALLTREE](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SMALLTREEID] [nvarchar](50) NULL,
	[PLOTID] [nvarchar](50) NULL,
	[SPECIESCODE] [int] NULL,
	[HT_CLASS1_COUNT] [int] NULL,
	[HT_CLASS2_COUNT] [int] NULL,
	[HT_CLASS3_COUNT] [int] NULL,
	[HT_CLASS4_COUNT] [int] NULL,
	[HT_CLASS5_COUNT] [int] NULL,
	[HT_CLASS6_COUNT] [int] NULL,
	[HT_CLASS7_COUNT] [int] NULL,
	[HT_CLASS8_COUNT] [int] NULL,
	[Created] [datetime2](7) NULL,
	[LastModified] [datetime2](7) NULL,
	[IsDeleted] [nvarchar](1) NULL,
	[CreatedAtServer] [datetime2](7) NULL,
	[LastModifiedAtServer] [datetime2](7) NULL,
	[HEIGHT] [float] NULL,
	[COUNT] [int] NULL,
	[ERRORCOUNT] [int] NULL,
	[ERRORMSG] [nvarchar](max) NULL,
 CONSTRAINT [PK__SMALLTRE__3214EC27AE06ECF2] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UC_SMALLTREEID] UNIQUE NONCLUSTERED 
(
	[SMALLTREEID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SOIL]    Script Date: 2022-05-30 6:43:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SOIL](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SOILID] [nvarchar](50) NULL,
	[PLOTID] [nvarchar](50) NULL,
	[HORIZONNUMBER] [int] NULL,
	[DEPTHFROM] [float] NULL,
	[DEPTHTO] [float] NULL,
	[HORIZON] [nvarchar](50) NULL,
	[DECOMPOSITIONCODE] [nvarchar](50) NULL,
	[MINERALTEXTURECODE] [nvarchar](50) NULL,
	[POREPATTERNCODE] [nvarchar](50) NULL,
	[STRUCTURE] [nvarchar](50) NULL,
	[MATRIXCOLOUR] [nvarchar](50) NULL,
	[MOTTLECOLOUR] [nvarchar](50) NULL,
	[PERCENTGRAVEL] [int] NULL,
	[PERCENTCOBBLE] [int] NULL,
	[PERCENTSTONE] [int] NULL,
	[GLEYCOLOUR] [nvarchar](50) NULL,
	[Created] [datetime2](7) NULL,
	[LastModified] [datetime2](7) NULL,
	[IsDeleted] [nvarchar](1) NULL,
	[CreatedAtServer] [datetime2](7) NULL,
	[LastModifiedAtServer] [datetime2](7) NULL,
	[ERRORCOUNT] [int] NULL,
	[ERRORMSG] [nvarchar](max) NULL,
 CONSTRAINT [PK__SOIL__3214EC2794E65EF9] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UC_SOILID] UNIQUE NONCLUSTERED 
(
	[SOILID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  View [dbo].[qryPlotStatus]    Script Date: 2022-05-30 6:43:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[qryPlotStatus]
AS
SELECT   TOP (100) PERCENT dbo.PLOT.VSNPLOTNAME, dbo.PLOT.VSNPLOTTYPECODE, dbo.PLOT.PLOTOVERVIEWDATE, dbo.PLOT.FIELD_CREW1, dbo.PLOT.ID, COUNT(DISTINCT dbo.SOIL.ID) 
                         AS [Soil Count], COUNT(DISTINCT dbo.ECOSITE.ID) AS [Ecosite Count], COUNT(DISTINCT dbo.PHOTO.ID) AS [Photo Count], COUNT(DISTINCT dbo.SMALLTREE.ID) 
                         AS [Small Tree Count]
FROM         dbo.PLOT LEFT OUTER JOIN
                         dbo.SMALLTREE ON dbo.PLOT.PLOTID = dbo.SMALLTREE.PLOTID LEFT OUTER JOIN
                         dbo.PHOTO ON dbo.PLOT.PLOTID = dbo.PHOTO.PLOTID LEFT OUTER JOIN
                         dbo.SOIL ON dbo.PLOT.PLOTID = dbo.SOIL.PLOTID LEFT OUTER JOIN
                         dbo.ECOSITE ON dbo.PLOT.PLOTID = dbo.ECOSITE.PLOTID
GROUP BY dbo.PLOT.VSNPLOTNAME, dbo.PLOT.VSNPLOTTYPECODE, dbo.PLOT.PLOTOVERVIEWDATE, dbo.PLOT.FIELD_CREW1, dbo.PLOT.ID
ORDER BY dbo.PLOT.VSNPLOTNAME, dbo.PLOT.PLOTOVERVIEWDATE, dbo.PLOT.VSNPLOTTYPECODE
GO
/****** Object:  Table [dbo].[DEFORMITY]    Script Date: 2022-05-30 6:43:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DEFORMITY](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[DEFORMITYID] [nvarchar](50) NULL,
	[TREEID] [nvarchar](50) NULL,
	[DEFORMITYTYPECODE] [int] NULL,
	[CAUSE] [int] NULL,
	[HEIGHTFROM] [float] NULL,
	[HEIGHTTO] [float] NULL,
	[QUADRANTCODE] [nvarchar](10) NULL,
	[EXTENT] [int] NULL,
	[DEGREELEANARCH] [int] NULL,
	[AZIMUTH] [int] NULL,
	[DEFORMITYLENGTH] [float] NULL,
	[DEFORMITYWIDTH] [float] NULL,
	[SCUFF] [int] NULL,
	[SCRAPE] [int] NULL,
	[GOUGE] [int] NULL,
	[OLD_FEEDING_CAVITY] [nvarchar](1) NULL,
	[NEW_FEEDING_CAVITY] [nvarchar](1) NULL,
	[OLD_NEST_CAVITY] [nvarchar](1) NULL,
	[NEW_NEST_CAVITY] [nvarchar](1) NULL,
	[STICK_NEST] [nvarchar](1) NULL,
	[Created] [datetime2](7) NULL,
	[LastModified] [datetime2](7) NULL,
	[IsDeleted] [nvarchar](1) NULL,
	[CreatedAtServer] [datetime2](7) NULL,
	[LastModifiedAtServer] [datetime2](7) NULL,
	[ERRORCOUNT] [int] NULL,
	[ERRORMSG] [nvarchar](max) NULL,
 CONSTRAINT [PK__DEFORMIT__3214EC27A4B8C2E0] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UC_DEFORMITYID] UNIQUE NONCLUSTERED 
(
	[DEFORMITYID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DWD]    Script Date: 2022-05-30 6:43:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DWD](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[DWDID] [nvarchar](50) NULL,
	[PLOTID] [nvarchar](50) NULL,
	[LINENUMBER] [int] NULL,
	[DWDNUM] [int] NULL,
	[SPECIESCODE] [int] NULL,
	[SMALLDIAMETER] [float] NULL,
	[DECOMPOSITIONCLASS] [int] NULL,
	[DOWNWOODYDEBRISORIGINCODE] [nvarchar](10) NULL,
	[TILTANGLE] [int] NULL,
	[DOWNWOODYDEBRISLENGTH] [float] NULL,
	[DIAMETER] [float] NULL,
	[LARGEDIAMETER] [float] NULL,
	[MOSS] [nvarchar](1) NULL,
	[BURNED] [nvarchar](1) NULL,
	[HOLLOW] [nvarchar](1) NULL,
	[IS_ACCUM] [nvarchar](1) NULL,
	[ACCUMULATIONLENGTH] [float] NULL,
	[ACCUMULATIONDEPTH] [float] NULL,
	[PERCENTCONIFER] [int] NULL,
	[PERCENTHARDWOOD] [int] NULL,
	[Created] [datetime2](7) NULL,
	[LastModified] [datetime2](7) NULL,
	[IsDeleted] [nvarchar](1) NULL,
	[CreatedAtServer] [datetime2](7) NULL,
	[LastModifiedAtServer] [datetime2](7) NULL,
	[ERRORCOUNT] [int] NULL,
	[ERRORMSG] [nvarchar](max) NULL,
 CONSTRAINT [PK__DWD__3214EC276389C0F4] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UC_DWDID] UNIQUE NONCLUSTERED 
(
	[DWDID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[STEMMAP]    Script Date: 2022-05-30 6:43:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[STEMMAP](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[STEMMAPID] [nvarchar](50) NULL,
	[TREEID] [nvarchar](50) NULL,
	[AZIMUTH] [int] NULL,
	[DISTANCE] [float] NULL,
	[CROWNWIDTH1] [float] NULL,
	[CROWNWIDTH2] [float] NULL,
	[CROWNOFFSETAZIMUTH] [int] NULL,
	[CROWNOFFSETDISTANCE] [float] NULL,
	[Created] [datetime2](7) NULL,
	[LastModified] [datetime2](7) NULL,
	[IsDeleted] [nvarchar](1) NULL,
	[CreatedAtServer] [datetime2](7) NULL,
	[LastModifiedAtServer] [datetime2](7) NULL,
	[ERRORCOUNT] [int] NULL,
	[ERRORMSG] [nvarchar](max) NULL,
 CONSTRAINT [PK__STEMMAP__3214EC27D1C97C52] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UC_STEMMAPID] UNIQUE NONCLUSTERED 
(
	[STEMMAPID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TREE]    Script Date: 2022-05-30 6:43:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TREE](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[TREEID] [nvarchar](50) NULL,
	[PLOTID] [nvarchar](50) NULL,
	[SECTION] [int] NULL,
	[TREENUMBER] [int] NULL,
	[SPECIESCODE] [int] NULL,
	[TAG_TYPE] [nvarchar](50) NULL,
	[TREEORIGINCODE] [nvarchar](10) NULL,
	[TREESTATUSCODE] [nvarchar](10) NULL,
	[VIGOURCODE] [int] NULL,
	[HEIGHTTODBH] [float] NULL,
	[DBH] [float] NULL,
	[HT] [float] NULL,
	[LENGTH] [float] NULL,
	[DBHIN] [nvarchar](1) NULL,
	[CROWNIN] [nvarchar](1) NULL,
	[LIVE_CROWN_RATIO] [int] NULL,
	[CROWNCLASSCODE] [nvarchar](10) NULL,
	[CROWN_POSITION] [int] NULL,
	[CROWNDAMAGECODE] [int] NULL,
	[DEFOLIATING_INSECT] [int] NULL,
	[FOLIAR_DISEASE] [int] NULL,
	[STEMQUALITYCODE] [nvarchar](10) NULL,
	[BARKRETENTIONCODE] [int] NULL,
	[WOODCONDITIONCODE] [int] NULL,
	[DECAYCLASS] [int] NULL,
	[MORTALITYCAUSECODE] [int] NULL,
	[BROKENTOP] [nvarchar](1) NULL,
	[AZIMUTH] [float] NULL,
	[DISTANCE] [float] NULL,
	[CROWNWIDTH1] [float] NULL,
	[CROWNWIDTH2] [float] NULL,
	[COMMENTS] [text] NULL,
	[DBH1] [float] NULL,
	[DBH2] [float] NULL,
	[DIRECTTOTALHEIGHT] [float] NULL,
	[OCULARTOTALHEIGHT] [float] NULL,
	[HEIGHTTODEADTIP] [float] NULL,
	[DIRECTHEIGHTTOCONTLIVECROWN] [float] NULL,
	[OCULARHEIGHTTOCONTLIVECROWN] [float] NULL,
	[DIRECTOFFSETDISTANCE] [float] NULL,
	[DEGREEOFLEAN] [int] NULL,
	[HEIGHTTOCORE] [float] NULL,
	[CORESTATUSCODE] [nvarchar](10) NULL,
	[SOUNDWOODLENGTH] [float] NULL,
	[FIELDAGE] [int] NULL,
	[OFFICERINGCOUNT] [int] NULL,
	[MISSINGRINGS] [int] NULL,
	[Created] [datetime2](7) NULL,
	[LastModified] [datetime2](7) NULL,
	[IsDeleted] [nvarchar](1) NULL,
	[CreatedAtServer] [datetime2](7) NULL,
	[LastModifiedAtServer] [datetime2](7) NULL,
	[VSNSTATUSCODE] [nvarchar](10) NULL,
	[ERRORCOUNT] [int] NULL,
	[ERRORMSG] [nvarchar](max) NULL,
 CONSTRAINT [PK__TREE__3214EC27CB3D7497] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UC_TREEID] UNIQUE NONCLUSTERED 
(
	[TREEID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  View [dbo].[qryStatus]    Script Date: 2022-05-30 6:43:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[qryStatus]
AS
SELECT   TOP (100) PERCENT dbo.PLOT.VSNPLOTNAME, dbo.PLOT.VSNPLOTTYPECODE, dbo.PLOT.PLOTOVERVIEWDATE, dbo.PLOT.FIELD_CREW1, dbo.PLOT.ID, COUNT(DISTINCT dbo.TREE.ID) 
                         AS [Tree Count], COUNT(DISTINCT dbo.DEFORMITY.ID) AS [Deformity Count], COUNT(DISTINCT dbo.STEMMAP.ID) AS [StemMap Count], COUNT(DISTINCT dbo.PHOTO.ID) 
                         AS [Photo Count], COUNT(DISTINCT dbo.ECOSITE.ID) AS [Ecosite Status], COUNT(DISTINCT dbo.DWD.ID) AS Expr1
FROM         dbo.PLOT INNER JOIN
                         dbo.TREE ON dbo.PLOT.PLOTID = dbo.TREE.PLOTID LEFT OUTER JOIN
                         dbo.DWD ON dbo.PLOT.PLOTID = dbo.DWD.PLOTID LEFT OUTER JOIN
                         dbo.ECOSITE ON dbo.PLOT.PLOTID = dbo.ECOSITE.PLOTID LEFT OUTER JOIN
                         dbo.PHOTO ON dbo.PLOT.PLOTID = dbo.PHOTO.PLOTID LEFT OUTER JOIN
                         dbo.STEMMAP ON dbo.TREE.TREEID = dbo.STEMMAP.TREEID LEFT OUTER JOIN
                         dbo.DEFORMITY ON dbo.TREE.TREEID = dbo.DEFORMITY.TREEID
GROUP BY dbo.PLOT.VSNPLOTNAME, dbo.PLOT.VSNPLOTTYPECODE, dbo.PLOT.PLOTOVERVIEWDATE, dbo.PLOT.FIELD_CREW1, dbo.PLOT.ID
ORDER BY dbo.PLOT.VSNPLOTNAME, dbo.PLOT.PLOTOVERVIEWDATE, dbo.PLOT.VSNPLOTTYPECODE
GO
/****** Object:  Table [dbo].[VEGETATION]    Script Date: 2022-05-30 6:43:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VEGETATION](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[VEGETATIONID] [nvarchar](50) NULL,
	[PLOTID] [nvarchar](50) NULL,
	[VSNSPECIESCODE] [nvarchar](50) NULL,
	[SPECIMENNUMBER] [int] NULL,
	[QUAD1] [float] NULL,
	[QUAD2] [float] NULL,
	[QUAD3] [float] NULL,
	[QUAD4] [float] NULL,
	[ELCLAYER3] [float] NULL,
	[ELCLAYER4] [float] NULL,
	[ELCLAYER5] [float] NULL,
	[ELCLAYER6] [float] NULL,
	[ELCLAYER7] [float] NULL,
	[Created] [datetime2](7) NULL,
	[LastModified] [datetime2](7) NULL,
	[IsDeleted] [nvarchar](1) NULL,
	[CreatedAtServer] [datetime2](7) NULL,
	[LastModifiedAtServer] [datetime2](7) NULL,
	[QUAD2_ELC4] [float] NULL,
	[QUAD3_ELC4] [float] NULL,
	[QUAD4_ELC4] [float] NULL,
	[QUAD1_ELC5] [float] NULL,
	[QUAD2_ELC5] [float] NULL,
	[QUAD3_ELC5] [float] NULL,
	[QUAD4_ELC5] [float] NULL,
	[QUAD1_ELC6] [float] NULL,
	[QUAD2_ELC6] [float] NULL,
	[QUAD3_ELC6] [float] NULL,
	[QUAD4_ELC6] [float] NULL,
	[QUAD1_ELC7] [float] NULL,
	[QUAD2_ELC7] [float] NULL,
	[QUAD3_ELC7] [float] NULL,
	[QUAD4_ELC7] [float] NULL,
	[QUAD1_ELC4] [float] NULL,
	[ERRORCOUNT] [int] NULL,
	[ERRORMSG] [nvarchar](max) NULL,
 CONSTRAINT [PK__VEGETATI__3214EC273B2FEE15] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UC_VEGETATIONID] UNIQUE NONCLUSTERED 
(
	[VEGETATIONID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[VEGETATIONCENSUS]    Script Date: 2022-05-30 6:43:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VEGETATIONCENSUS](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[VEGETATIONCENSUSID] [nvarchar](50) NULL,
	[PLOTID] [nvarchar](50) NULL,
	[VSNSPECIESCODE] [nvarchar](50) NULL,
	[SPECIMENNUMBER] [int] NULL,
	[Created] [datetime2](7) NULL,
	[LastModified] [datetime2](7) NULL,
	[IsDeleted] [nvarchar](1) NULL,
	[CreatedAtServer] [datetime2](7) NULL,
	[LastModifiedAtServer] [datetime2](7) NULL,
	[ERRORCOUNT] [int] NULL,
	[ERRORMSG] [nvarchar](max) NULL,
 CONSTRAINT [PK__VEGETATI__3214EC2761E950A7] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UC_VEGETATIONCENSUSID] UNIQUE NONCLUSTERED 
(
	[VEGETATIONCENSUSID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  View [dbo].[qryDWDStatus]    Script Date: 2022-05-30 6:43:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[qryDWDStatus]
AS
SELECT   TOP (100) PERCENT dbo.PLOT.VSNPLOTNAME, dbo.PLOT.VSNPLOTTYPECODE, dbo.PLOT.PLOTOVERVIEWDATE, dbo.PLOT.FIELD_CREW1, dbo.PLOT.ID, COUNT(DISTINCT dbo.DWD.ID) 
                         AS [DWD Count], COUNT(DISTINCT dbo.VEGETATION.ID) AS [Veg Count], COUNT(DISTINCT dbo.VEGETATIONCENSUS.ID) AS [Veg Census Count]
FROM         dbo.PLOT LEFT OUTER JOIN
                         dbo.VEGETATION ON dbo.PLOT.PLOTID = dbo.VEGETATION.PLOTID LEFT OUTER JOIN
                         dbo.VEGETATIONCENSUS ON dbo.PLOT.PLOTID = dbo.VEGETATIONCENSUS.PLOTID LEFT OUTER JOIN
                         dbo.DWD ON dbo.PLOT.PLOTID = dbo.DWD.PLOTID
GROUP BY dbo.PLOT.VSNPLOTNAME, dbo.PLOT.VSNPLOTTYPECODE, dbo.PLOT.PLOTOVERVIEWDATE, dbo.PLOT.FIELD_CREW1, dbo.PLOT.ID
ORDER BY dbo.PLOT.VSNPLOTNAME, dbo.PLOT.PLOTOVERVIEWDATE, dbo.PLOT.VSNPLOTTYPECODE
GO
/****** Object:  View [dbo].[qryTreeStatus]    Script Date: 2022-05-30 6:43:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[qryTreeStatus]
AS
SELECT        TOP (100) PERCENT dbo.PLOT.VSNPLOTNAME, dbo.PLOT.VSNPLOTTYPECODE, dbo.PLOT.PLOTOVERVIEWDATE, dbo.PLOT.FIELD_CREW1, dbo.PLOT.ID, COUNT(DISTINCT dbo.TREE.ID) AS [Tree Count], 
                         COUNT(DISTINCT dbo.DEFORMITY.ID) AS [Deformity Count], COUNT(DISTINCT dbo.STEMMAP.ID) AS [StemMap Count]
FROM            dbo.PLOT LEFT OUTER JOIN
                         dbo.TREE ON dbo.PLOT.PLOTID = dbo.TREE.PLOTID LEFT OUTER JOIN
                         dbo.STEMMAP ON dbo.TREE.TREEID = dbo.STEMMAP.TREEID LEFT OUTER JOIN
                         dbo.DEFORMITY ON dbo.TREE.TREEID = dbo.DEFORMITY.TREEID
GROUP BY dbo.PLOT.VSNPLOTNAME, dbo.PLOT.VSNPLOTTYPECODE, dbo.PLOT.PLOTOVERVIEWDATE, dbo.PLOT.FIELD_CREW1, dbo.PLOT.ID
ORDER BY dbo.PLOT.VSNPLOTNAME, dbo.PLOT.PLOTOVERVIEWDATE, dbo.PLOT.VSNPLOTTYPECODE
GO
/****** Object:  View [dbo].[qryOverallStatus]    Script Date: 2022-05-30 6:43:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[qryOverallStatus]
AS
SELECT        TOP (100) PERCENT dbo.qryTreeStatus.VSNPLOTNAME, dbo.qryTreeStatus.VSNPLOTTYPECODE, dbo.qryTreeStatus.PLOTOVERVIEWDATE, dbo.qryTreeStatus.FIELD_CREW1, dbo.qryTreeStatus.ID, 
                         dbo.qryTreeStatus.[Tree Count], dbo.qryTreeStatus.[Deformity Count], dbo.qryTreeStatus.[StemMap Count], dbo.qryPlotStatus.[Ecosite Count], dbo.qryPlotStatus.[Soil Count], dbo.qryPlotStatus.[Photo Count], 
                         dbo.qryPlotStatus.[Small Tree Count], dbo.qryDWDStatus.[DWD Count], dbo.qryDWDStatus.[Veg Count], dbo.qryDWDStatus.[Veg Census Count]
FROM            dbo.qryTreeStatus INNER JOIN
                         dbo.qryPlotStatus ON dbo.qryTreeStatus.ID = dbo.qryPlotStatus.ID INNER JOIN
                         dbo.qryDWDStatus ON dbo.qryTreeStatus.ID = dbo.qryDWDStatus.ID
ORDER BY dbo.qryTreeStatus.PLOTOVERVIEWDATE DESC, dbo.qryTreeStatus.VSNPLOTNAME
GO
/****** Object:  View [dbo].[View_1]    Script Date: 2022-05-30 6:43:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[View_1]
AS
SELECT        TOP (100) PERCENT dbo.PLOT.VSNPLOTNAME, dbo.PLOT.VSNPLOTTYPECODE, dbo.PLOT.PLOTOVERVIEWDATE, dbo.PLOT.FIELD_CREW1, dbo.PLOT.ID, COUNT(dbo.TREE.ID) AS [Tree Count]
FROM            dbo.PLOT INNER JOIN
                         dbo.TREE ON dbo.PLOT.PLOTID = dbo.TREE.PLOTID
GROUP BY dbo.PLOT.VSNPLOTNAME, dbo.PLOT.VSNPLOTTYPECODE, dbo.PLOT.PLOTOVERVIEWDATE, dbo.PLOT.FIELD_CREW1, dbo.PLOT.ID
ORDER BY dbo.PLOT.VSNPLOTNAME, dbo.PLOT.PLOTOVERVIEWDATE, dbo.PLOT.VSNPLOTTYPECODE
GO
/****** Object:  Table [dbo].[PERSON]    Script Date: 2022-05-30 6:43:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PERSON](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PERSONID] [nvarchar](50) NULL,
	[PROJECTID] [nvarchar](50) NULL,
	[FIRSTNAME] [nvarchar](50) NULL,
	[LASTNAME] [nvarchar](50) NULL,
	[Created] [datetime2](7) NULL,
	[LastModified] [datetime2](7) NULL,
	[IsDeleted] [nvarchar](1) NULL,
	[CreatedAtServer] [datetime2](7) NULL,
	[LastModifiedAtServer] [datetime2](7) NULL,
 CONSTRAINT [PK__PERSON__3214EC27D66EA52B] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UC_PERSONID] UNIQUE NONCLUSTERED 
(
	[PERSONID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[tlkpPerson]    Script Date: 2022-05-30 6:43:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[tlkpPerson]
AS
SELECT ID AS PersonCode, FIRSTNAME, LASTNAME, { fn CONCAT(CAST(LASTNAME AS nvarchar), CAST(FIRSTNAME AS nvarchar)) } AS FullName
FROM     dbo.PERSON
GO
/****** Object:  View [dbo].[qryOverview]    Script Date: 2022-05-30 6:43:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[qryOverview]
AS
SELECT   TOP (100) PERCENT PLOT.VSNPLOTNAME, PLOT.VSNPLOTTYPECODE, PLOT.FIELD_CREW1, PLOT.ID, COUNT(TREE.ID) AS TreeCount, COUNT(STEMMAP.ID) AS [Stem Map count], 
                         COUNT(DEFORMITY.ID) AS [Deformity/Cavity Count], COUNT(ECOSITE.ID) AS CountOfID, COUNT(SOIL.ID) AS CountOfID1, COUNT(VEGETATIONCENSUS.ID) AS CountOfID2, 
                         COUNT(VEGETATION.ID) AS CountOfID3, COUNT(PHOTO.ID) AS CountOfID4, COUNT(SMALLTREE.ID) AS CountOfID5, COUNT(DWD.ID) AS CountOfID6
FROM         dbo.PLOT INNER JOIN
                         dbo.TREE ON PLOT.PLOTID = TREE.PLOTID LEFT OUTER JOIN
                         dbo.DEFORMITY ON TREE.TREEID = DEFORMITY.TREEID LEFT OUTER JOIN
                         dbo.STEMMAP ON TREE.TREEID = STEMMAP.TREEID LEFT OUTER JOIN
                         dbo.ECOSITE ON PLOT.PLOTID = ECOSITE.PLOTID LEFT OUTER JOIN
                         dbo.SOIL ON PLOT.PLOTID = SOIL.PLOTID LEFT OUTER JOIN
                         dbo.SMALLTREE ON PLOT.PLOTID = SMALLTREE.PLOTID LEFT OUTER JOIN
                         dbo.PHOTO ON PLOT.PLOTID = PHOTO.PLOTID LEFT OUTER JOIN
                         dbo.VEGETATION ON PLOT.PLOTID = VEGETATION.PLOTID LEFT OUTER JOIN
                         dbo.VEGETATIONCENSUS ON PLOT.PLOTID = VEGETATIONCENSUS.PLOTID LEFT OUTER JOIN
                         dbo.DWD ON PLOT.PLOTID = DWD.PLOTID
GROUP BY PLOT.VSNPLOTNAME, PLOT.VSNPLOTTYPECODE, PLOT.FIELD_CREW1, PLOT.ID
ORDER BY PLOT.VSNPLOTNAME, PLOT.VSNPLOTTYPECODE
GO
/****** Object:  Table [dbo].[PROJECT]    Script Date: 2022-05-30 6:43:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PROJECT](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PROJECTID] [nvarchar](50) NULL,
	[NAME] [text] NULL,
	[DESCRIPTION] [text] NULL,
	[PROJECT_DATE] [nvarchar](50) NULL,
	[Created] [datetime2](7) NULL,
	[LastModified] [datetime2](7) NULL,
	[IsDeleted] [nvarchar](1) NULL,
	[CreatedAtServer] [datetime2](7) NULL,
	[LastModifiedAtServer] [datetime2](7) NULL,
 CONSTRAINT [PK__PROJECT__3214EC2765F10217] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UC_PROJECTID] UNIQUE NONCLUSTERED 
(
	[PROJECTID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SMALLTREETALLY]    Script Date: 2022-05-30 6:43:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SMALLTREETALLY](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SMALLTREETALLYID] [nvarchar](50) NULL,
	[PLOTID] [nvarchar](50) NULL,
	[SPECIESCODE] [int] NULL,
	[DBH] [float] NULL,
	[HEIGHT] [float] NULL,
	[COUNT] [int] NULL,
	[Created] [datetime2](7) NULL,
	[LastModified] [datetime2](7) NULL,
	[IsDeleted] [nvarchar](1) NULL,
	[CreatedAtServer] [datetime2](7) NULL,
	[LastModifiedAtServer] [datetime2](7) NULL,
	[ERRORCOUNT] [int] NULL,
	[ERRORMSG] [nvarchar](max) NULL,
 CONSTRAINT [PK__SMALLTREETALLY__3214EC27AE06ECF2] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UC_SMALLTREETALLYID] UNIQUE NONCLUSTERED 
(
	[SMALLTREETALLYID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[STATUS]    Script Date: 2022-05-30 6:43:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[STATUS](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PLOTID] [nvarchar](50) NOT NULL,
	[VSNPLOTNAME] [nvarchar](20) NOT NULL,
	[Crew_Complete] [datetime2](7) NULL,
	[Age_Complete] [datetime2](7) NULL,
	[IA_Complete] [datetime2](7) NULL,
	[Contractor_QC] [datetime2](7) NULL,
	[Contractor_Complete] [datetime2](7) NULL,
	[Contractor_Invoiced] [datetime2](7) NULL,
	[Ministry_Data_Accepted] [datetime2](7) NULL,
	[Ministry_Soils_Received] [datetime2](7) NULL,
	[Ministry_Location_OK] [datetime2](7) NULL,
	[Ministry_Plot_Acceptable] [datetime2](7) NULL,
 CONSTRAINT [PK_STATUS] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [deftreeid]    Script Date: 2022-05-30 6:43:43 PM ******/
CREATE NONCLUSTERED INDEX [deftreeid] ON [dbo].[DEFORMITY]
(
	[TREEID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [dwdplotid]    Script Date: 2022-05-30 6:43:43 PM ******/
CREATE NONCLUSTERED INDEX [dwdplotid] ON [dbo].[DWD]
(
	[PLOTID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [ecositeplotid]    Script Date: 2022-05-30 6:43:43 PM ******/
CREATE NONCLUSTERED INDEX [ecositeplotid] ON [dbo].[ECOSITE]
(
	[PLOTID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [photoplotid]    Script Date: 2022-05-30 6:43:43 PM ******/
CREATE NONCLUSTERED INDEX [photoplotid] ON [dbo].[PHOTO]
(
	[PLOTID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [smalltreeplotid]    Script Date: 2022-05-30 6:43:43 PM ******/
CREATE NONCLUSTERED INDEX [smalltreeplotid] ON [dbo].[SMALLTREE]
(
	[PLOTID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [soilplotid]    Script Date: 2022-05-30 6:43:43 PM ******/
CREATE NONCLUSTERED INDEX [soilplotid] ON [dbo].[SOIL]
(
	[PLOTID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_STATUS]    Script Date: 2022-05-30 6:43:43 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_STATUS] ON [dbo].[STATUS]
(
	[PLOTID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [stemtreeid]    Script Date: 2022-05-30 6:43:43 PM ******/
CREATE NONCLUSTERED INDEX [stemtreeid] ON [dbo].[STEMMAP]
(
	[TREEID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [vegplotid]    Script Date: 2022-05-30 6:43:43 PM ******/
CREATE NONCLUSTERED INDEX [vegplotid] ON [dbo].[VEGETATION]
(
	[PLOTID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [vegcensusplotid]    Script Date: 2022-05-30 6:43:43 PM ******/
CREATE NONCLUSTERED INDEX [vegcensusplotid] ON [dbo].[VEGETATIONCENSUS]
(
	[PLOTID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[DEFORMITY] ADD  CONSTRAINT [DF__DEFORMITY__Creat__7CD98669]  DEFAULT (getdate()) FOR [Created]
GO
ALTER TABLE [dbo].[DEFORMITY] ADD  CONSTRAINT [DF__DEFORMITY__LastM__7DCDAAA2]  DEFAULT (getdate()) FOR [LastModified]
GO
ALTER TABLE [dbo].[DEFORMITY] ADD  CONSTRAINT [DF__DEFORMITY__IsDel__7EC1CEDB]  DEFAULT ('N') FOR [IsDeleted]
GO
ALTER TABLE [dbo].[DEFORMITY] ADD  CONSTRAINT [DF__DEFORMITY__Creat__7FB5F314]  DEFAULT (getdate()) FOR [CreatedAtServer]
GO
ALTER TABLE [dbo].[DEFORMITY] ADD  CONSTRAINT [DF__DEFORMITY__LastM__00AA174D]  DEFAULT (getdate()) FOR [LastModifiedAtServer]
GO
ALTER TABLE [dbo].[DWD] ADD  CONSTRAINT [DF__DWD__Created__047AA831]  DEFAULT (getdate()) FOR [Created]
GO
ALTER TABLE [dbo].[DWD] ADD  CONSTRAINT [DF__DWD__LastModifie__056ECC6A]  DEFAULT (getdate()) FOR [LastModified]
GO
ALTER TABLE [dbo].[DWD] ADD  CONSTRAINT [DF__DWD__IsDeleted__0662F0A3]  DEFAULT ('N') FOR [IsDeleted]
GO
ALTER TABLE [dbo].[DWD] ADD  CONSTRAINT [DF__DWD__CreatedAtSe__075714DC]  DEFAULT (getdate()) FOR [CreatedAtServer]
GO
ALTER TABLE [dbo].[DWD] ADD  CONSTRAINT [DF__DWD__LastModifie__084B3915]  DEFAULT (getdate()) FOR [LastModifiedAtServer]
GO
ALTER TABLE [dbo].[ECOSITE] ADD  CONSTRAINT [DF__ECOSITE__Created__2AA05119]  DEFAULT (getdate()) FOR [Created]
GO
ALTER TABLE [dbo].[ECOSITE] ADD  CONSTRAINT [DF__ECOSITE__LastMod__2B947552]  DEFAULT (getdate()) FOR [LastModified]
GO
ALTER TABLE [dbo].[ECOSITE] ADD  CONSTRAINT [DF__ECOSITE__IsDelet__2C88998B]  DEFAULT ('N') FOR [IsDeleted]
GO
ALTER TABLE [dbo].[ECOSITE] ADD  CONSTRAINT [DF__ECOSITE__Created__2D7CBDC4]  DEFAULT (getdate()) FOR [CreatedAtServer]
GO
ALTER TABLE [dbo].[ECOSITE] ADD  CONSTRAINT [DF__ECOSITE__LastMod__2E70E1FD]  DEFAULT (getdate()) FOR [LastModifiedAtServer]
GO
ALTER TABLE [dbo].[PERSON] ADD  CONSTRAINT [DF__PERSON__Created__308E3499]  DEFAULT (getdate()) FOR [Created]
GO
ALTER TABLE [dbo].[PERSON] ADD  CONSTRAINT [DF__PERSON__LastModi__318258D2]  DEFAULT (getdate()) FOR [LastModified]
GO
ALTER TABLE [dbo].[PERSON] ADD  CONSTRAINT [DF__PERSON__IsDelete__32767D0B]  DEFAULT ('N') FOR [IsDeleted]
GO
ALTER TABLE [dbo].[PERSON] ADD  CONSTRAINT [DF__PERSON__CreatedA__336AA144]  DEFAULT (getdate()) FOR [CreatedAtServer]
GO
ALTER TABLE [dbo].[PERSON] ADD  CONSTRAINT [DF__PERSON__LastModi__345EC57D]  DEFAULT (getdate()) FOR [LastModifiedAtServer]
GO
ALTER TABLE [dbo].[PHOTO] ADD  CONSTRAINT [DF__PHOTO__Created__753864A1]  DEFAULT (getdate()) FOR [Created]
GO
ALTER TABLE [dbo].[PHOTO] ADD  CONSTRAINT [DF__PHOTO__LastModif__762C88DA]  DEFAULT (getdate()) FOR [LastModified]
GO
ALTER TABLE [dbo].[PHOTO] ADD  CONSTRAINT [DF__PHOTO__IsDeleted__7720AD13]  DEFAULT ('N') FOR [IsDeleted]
GO
ALTER TABLE [dbo].[PHOTO] ADD  CONSTRAINT [DF__PHOTO__CreatedAt__7814D14C]  DEFAULT (getdate()) FOR [CreatedAtServer]
GO
ALTER TABLE [dbo].[PHOTO] ADD  CONSTRAINT [DF__PHOTO__LastModif__7908F585]  DEFAULT (getdate()) FOR [LastModifiedAtServer]
GO
ALTER TABLE [dbo].[PLOT] ADD  CONSTRAINT [DF__PLOT__Created__22FF2F51]  DEFAULT (getdate()) FOR [Created]
GO
ALTER TABLE [dbo].[PLOT] ADD  CONSTRAINT [DF__PLOT__LastModifi__23F3538A]  DEFAULT (getdate()) FOR [LastModified]
GO
ALTER TABLE [dbo].[PLOT] ADD  CONSTRAINT [DF__PLOT__IsDeleted__24E777C3]  DEFAULT ('N') FOR [IsDeleted]
GO
ALTER TABLE [dbo].[PLOT] ADD  CONSTRAINT [DF__PLOT__CreatedAtS__25DB9BFC]  DEFAULT (getdate()) FOR [CreatedAtServer]
GO
ALTER TABLE [dbo].[PLOT] ADD  CONSTRAINT [DF__PLOT__LastModifi__26CFC035]  DEFAULT (getdate()) FOR [LastModifiedAtServer]
GO
ALTER TABLE [dbo].[PROJECT] ADD  CONSTRAINT [DF__PROJECT__Created__382F5661]  DEFAULT (getdate()) FOR [Created]
GO
ALTER TABLE [dbo].[PROJECT] ADD  CONSTRAINT [DF__PROJECT__LastMod__39237A9A]  DEFAULT (getdate()) FOR [LastModified]
GO
ALTER TABLE [dbo].[PROJECT] ADD  CONSTRAINT [DF__PROJECT__IsDelet__3A179ED3]  DEFAULT ('N') FOR [IsDeleted]
GO
ALTER TABLE [dbo].[PROJECT] ADD  CONSTRAINT [DF__PROJECT__Created__3B0BC30C]  DEFAULT (getdate()) FOR [CreatedAtServer]
GO
ALTER TABLE [dbo].[PROJECT] ADD  CONSTRAINT [DF__PROJECT__LastMod__3BFFE745]  DEFAULT (getdate()) FOR [LastModifiedAtServer]
GO
ALTER TABLE [dbo].[SMALLTREE] ADD  CONSTRAINT [DF__SMALLTREE__Creat__6D9742D9]  DEFAULT (getdate()) FOR [Created]
GO
ALTER TABLE [dbo].[SMALLTREE] ADD  CONSTRAINT [DF__SMALLTREE__LastM__6E8B6712]  DEFAULT (getdate()) FOR [LastModified]
GO
ALTER TABLE [dbo].[SMALLTREE] ADD  CONSTRAINT [DF__SMALLTREE__IsDel__6F7F8B4B]  DEFAULT ('N') FOR [IsDeleted]
GO
ALTER TABLE [dbo].[SMALLTREE] ADD  CONSTRAINT [DF__SMALLTREE__Creat__7073AF84]  DEFAULT (getdate()) FOR [CreatedAtServer]
GO
ALTER TABLE [dbo].[SMALLTREE] ADD  CONSTRAINT [DF__SMALLTREE__LastM__7167D3BD]  DEFAULT (getdate()) FOR [LastModifiedAtServer]
GO
ALTER TABLE [dbo].[SMALLTREETALLY] ADD  CONSTRAINT [DF__SMALLTREETALLY__Creat__6D9742D9]  DEFAULT (getdate()) FOR [Created]
GO
ALTER TABLE [dbo].[SMALLTREETALLY] ADD  CONSTRAINT [DF__SMALLTREETALLY__LastM__6E8B6712]  DEFAULT (getdate()) FOR [LastModified]
GO
ALTER TABLE [dbo].[SMALLTREETALLY] ADD  CONSTRAINT [DF__SMALLTREETALLY__IsDel__6F7F8B4B]  DEFAULT ('N') FOR [IsDeleted]
GO
ALTER TABLE [dbo].[SMALLTREETALLY] ADD  CONSTRAINT [DF__SMALLTREETALLY__Creat__7073AF84]  DEFAULT (getdate()) FOR [CreatedAtServer]
GO
ALTER TABLE [dbo].[SMALLTREETALLY] ADD  CONSTRAINT [DF__SMALLTREETALLY__LastM__7167D3BD]  DEFAULT (getdate()) FOR [LastModifiedAtServer]
GO
ALTER TABLE [dbo].[SOIL] ADD  CONSTRAINT [DF__SOIL__Created__65F62111]  DEFAULT (getdate()) FOR [Created]
GO
ALTER TABLE [dbo].[SOIL] ADD  CONSTRAINT [DF__SOIL__LastModifi__66EA454A]  DEFAULT (getdate()) FOR [LastModified]
GO
ALTER TABLE [dbo].[SOIL] ADD  CONSTRAINT [DF__SOIL__IsDeleted__67DE6983]  DEFAULT ('N') FOR [IsDeleted]
GO
ALTER TABLE [dbo].[SOIL] ADD  CONSTRAINT [DF__SOIL__CreatedAtS__68D28DBC]  DEFAULT (getdate()) FOR [CreatedAtServer]
GO
ALTER TABLE [dbo].[SOIL] ADD  CONSTRAINT [DF__SOIL__LastModifi__69C6B1F5]  DEFAULT (getdate()) FOR [LastModifiedAtServer]
GO
ALTER TABLE [dbo].[STEMMAP] ADD  CONSTRAINT [DF__STEMMAP__Created__1B5E0D89]  DEFAULT (getdate()) FOR [Created]
GO
ALTER TABLE [dbo].[STEMMAP] ADD  CONSTRAINT [DF__STEMMAP__LastMod__1C5231C2]  DEFAULT (getdate()) FOR [LastModified]
GO
ALTER TABLE [dbo].[STEMMAP] ADD  CONSTRAINT [DF__STEMMAP__IsDelet__1D4655FB]  DEFAULT ('N') FOR [IsDeleted]
GO
ALTER TABLE [dbo].[STEMMAP] ADD  CONSTRAINT [DF__STEMMAP__Created__1E3A7A34]  DEFAULT (getdate()) FOR [CreatedAtServer]
GO
ALTER TABLE [dbo].[STEMMAP] ADD  CONSTRAINT [DF__STEMMAP__LastMod__1F2E9E6D]  DEFAULT (getdate()) FOR [LastModifiedAtServer]
GO
ALTER TABLE [dbo].[TREE] ADD  CONSTRAINT [DF__TREE__Created__56B3DD81]  DEFAULT (getdate()) FOR [Created]
GO
ALTER TABLE [dbo].[TREE] ADD  CONSTRAINT [DF__TREE__LastModifi__57A801BA]  DEFAULT (getdate()) FOR [LastModified]
GO
ALTER TABLE [dbo].[TREE] ADD  CONSTRAINT [DF__TREE__IsDeleted__589C25F3]  DEFAULT ('N') FOR [IsDeleted]
GO
ALTER TABLE [dbo].[TREE] ADD  CONSTRAINT [DF__TREE__CreatedAtS__59904A2C]  DEFAULT (getdate()) FOR [CreatedAtServer]
GO
ALTER TABLE [dbo].[TREE] ADD  CONSTRAINT [DF__TREE__LastModifi__5A846E65]  DEFAULT (getdate()) FOR [LastModifiedAtServer]
GO
ALTER TABLE [dbo].[VEGETATION] ADD  CONSTRAINT [DF__VEGETATIO__Creat__0C1BC9F9]  DEFAULT (getdate()) FOR [Created]
GO
ALTER TABLE [dbo].[VEGETATION] ADD  CONSTRAINT [DF__VEGETATIO__LastM__0D0FEE32]  DEFAULT (getdate()) FOR [LastModified]
GO
ALTER TABLE [dbo].[VEGETATION] ADD  CONSTRAINT [DF__VEGETATIO__IsDel__0E04126B]  DEFAULT ('N') FOR [IsDeleted]
GO
ALTER TABLE [dbo].[VEGETATION] ADD  CONSTRAINT [DF__VEGETATIO__Creat__0EF836A4]  DEFAULT (getdate()) FOR [CreatedAtServer]
GO
ALTER TABLE [dbo].[VEGETATION] ADD  CONSTRAINT [DF__VEGETATIO__LastM__0FEC5ADD]  DEFAULT (getdate()) FOR [LastModifiedAtServer]
GO
ALTER TABLE [dbo].[VEGETATIONCENSUS] ADD  CONSTRAINT [DF__VEGETATIO__Creat__13BCEBC1]  DEFAULT (getdate()) FOR [Created]
GO
ALTER TABLE [dbo].[VEGETATIONCENSUS] ADD  CONSTRAINT [DF__VEGETATIO__LastM__14B10FFA]  DEFAULT (getdate()) FOR [LastModified]
GO
ALTER TABLE [dbo].[VEGETATIONCENSUS] ADD  CONSTRAINT [DF__VEGETATIO__IsDel__15A53433]  DEFAULT ('N') FOR [IsDeleted]
GO
ALTER TABLE [dbo].[VEGETATIONCENSUS] ADD  CONSTRAINT [DF__VEGETATIO__Creat__1699586C]  DEFAULT (getdate()) FOR [CreatedAtServer]
GO
ALTER TABLE [dbo].[VEGETATIONCENSUS] ADD  CONSTRAINT [DF__VEGETATIO__LastM__178D7CA5]  DEFAULT (getdate()) FOR [LastModifiedAtServer]
GO
/****** Object:  StoredProcedure [dbo].[InsertDeformity]    Script Date: 2022-05-30 6:43:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[InsertDeformity](@fromjson nvarchar(max))
AS BEGIN

  BEGIN TRY
    insert into DEFORMITY(DEFORMITYID, TREEID, DEFORMITYTYPECODE, CAUSE,HEIGHTFROM,HEIGHTTO, QUADRANTCODE,EXTENT,DEGREELEANARCH,AZIMUTH,DEFORMITYLENGTH,
DEFORMITYWIDTH,SCUFF,SCRAPE,GOUGE,OLD_FEEDING_CAVITY,NEW_FEEDING_CAVITY,OLD_NEST_CAVITY,NEW_NEST_CAVITY,STICK_NEST,Created,LastModified,IsDeleted,ERRORCOUNT,
ERRORMSG )
    select j.DEFORMITYID, j.TREEID, j.DEFORMITYTYPECODE, j.CAUSE,j.HEIGHTFROM,j.HEIGHTTO, j.QUADRANTCODE,j.EXTENT,j.DEGREELEANARCH,j.AZIMUTH,j.DEFORMITYLENGTH,
j.DEFORMITYWIDTH,j.SCUFF,j.SCRAPE,j.GOUGE,j.OLD_FEEDING_CAVITY,j.NEW_FEEDING_CAVITY,j.OLD_NEST_CAVITY,j.NEW_NEST_CAVITY,j.STICK_NEST,j.Created,j.LastModified,j.IsDeleted,j.ERRORCOUNT,
j.ERRORMSG
    FROM OPENJSON (@fromjson)
     WITH (
DEFORMITYID nvarchar(50),
TREEID nvarchar(50),
DEFORMITYTYPECODE int,
CAUSE int,
HEIGHTFROM float,
HEIGHTTO float,
QUADRANTCODE nvarchar(10),
EXTENT int,
DEGREELEANARCH int,
AZIMUTH int,
DEFORMITYLENGTH float,
DEFORMITYWIDTH float,
SCUFF int,
SCRAPE int,
GOUGE int,
OLD_FEEDING_CAVITY nvarchar(1),
NEW_FEEDING_CAVITY nvarchar(1),
OLD_NEST_CAVITY nvarchar(1),
NEW_NEST_CAVITY nvarchar(1),
STICK_NEST nvarchar(1),
Created datetime2,
LastModified datetime2,
IsDeleted nvarchar(1),
ERRORCOUNT int,
ERRORMSG nvarchar(MAX)
) j left join DEFORMITY ON j.DEFORMITYID = DEFORMITY.DEFORMITYID where DEFORMITY.DEFORMITYID is null
	 RETURN 1
  END TRY
  BEGIN CATCH
     RETURN 0
  END CATCH

END
GO
/****** Object:  StoredProcedure [dbo].[InsertDWD]    Script Date: 2022-05-30 6:43:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[InsertDWD](@fromjson nvarchar(max))
AS BEGIN

  BEGIN TRY
    insert into DWD(DWDID,PLOTID,LINENUMBER,DWDNUM,SPECIESCODE,SMALLDIAMETER,DECOMPOSITIONCLASS,DOWNWOODYDEBRISORIGINCODE,TILTANGLE,DOWNWOODYDEBRISLENGTH,DIAMETER,LARGEDIAMETER,MOSS,BURNED,HOLLOW,IS_ACCUM,
ACCUMULATIONLENGTH,ACCUMULATIONDEPTH,PERCENTCONIFER,PERCENTHARDWOOD, Created, LastModified, IsDeleted,ERRORCOUNT,
ERRORMSG)
    select j.DWDID,j.PLOTID,j.LINENUMBER,j.DWDNUM,j.SPECIESCODE,j.SMALLDIAMETER,j.DECOMPOSITIONCLASS,j.DOWNWOODYDEBRISORIGINCODE,j.TILTANGLE,j.DOWNWOODYDEBRISLENGTH,j.DIAMETER,j.LARGEDIAMETER,j.MOSS,j.BURNED,j.HOLLOW,j.IS_ACCUM,
j.ACCUMULATIONLENGTH,j.ACCUMULATIONDEPTH,j.PERCENTCONIFER,j.PERCENTHARDWOOD, j.Created, j.LastModified, j.IsDeleted, j.ERRORCOUNT ,
j.ERRORMSG
    FROM OPENJSON (@fromjson)
     WITH (
    DWDID nvarchar(50),
	PLOTID nvarchar(50) ,
	LINENUMBER int,
	DWDNUM int,
	SPECIESCODE int,
	SMALLDIAMETER float,
	DECOMPOSITIONCLASS int,
	DOWNWOODYDEBRISORIGINCODE nvarchar(10),
	TILTANGLE int,
	[DOWNWOODYDEBRISLENGTH] float,
	DIAMETER float,
	LARGEDIAMETER float,
	MOSS nvarchar(1),
	BURNED nvarchar(1),
	HOLLOW nvarchar(1),
	IS_ACCUM nvarchar(1),
	ACCUMULATIONLENGTH float,
	ACCUMULATIONDEPTH float,
	PERCENTCONIFER int,
	PERCENTHARDWOOD int,
	Created datetime2,
	LastModified datetime2,
	IsDeleted nvarchar(1),
	ERRORCOUNT int,
ERRORMSG nvarchar(MAX)
     ) j left join DWD ON j.DWDID = DWD.DWDID where DWD.DWDID is null
	 RETURN 1
  END TRY
  BEGIN CATCH
     RETURN 0
  END CATCH

END
GO
/****** Object:  StoredProcedure [dbo].[InsertEcosite]    Script Date: 2022-05-30 6:43:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[InsertEcosite](@fromjson nvarchar(max))
AS BEGIN

  BEGIN TRY
    insert into ECOSITE(ECOSITEID,PLOTID,HUMUSFORMCODE,DRAINAGECLASSCODE,STRATIFIED,EFFECTIVE_PORE_PATTERN,ELC_SUBSTRATE_TYPE,DEPTHTODISTINCTMOTTLES,DEPTHTOPROMINENTMOTTLES,
DEPTHTOGLEY,DEPTHTOBEDROCK,DEPTHTOCARBONATES,MOISTURE_REGIME_DEPTH_CLASS,MOISTUREREGIMECODE,MODEOFDEPOSITIONCODE1,MODEOFDEPOSITIONCODE2,FUNCTIONALROOTINGDEPTH,
MAXIMUMROOTINGDEPTH,DEPTHTOROOTRESTRICTION,DEPTHTOWATERTABLE,DEPTHTOIMPASSABLECOARSEFRAGMENTS,PROBLEMATICSITE,DEPTHTOSEEPAGE,SOIL_PIT_PHOTO,PRI_ECO,
PRI_ECO_PCT,SEC_ECO,SEC_ECO_PCT,AZIMUTH,DISTANCE,SOIL_PIT_PHOTO1,SOIL_PIT_PHOTO2,SUBSTRATENOTE,POREPATTERNCODE,MINERALTEXTURECODE,DECOMPOSITIONCODE,
PITAZIMUTH,PITDISTANCE,SUBSTRATEDATE, SUBSTRATEPERSON,  Created, LastModified,IsDeleted,ERRORCOUNT,ERRORMSG )
    select j.ECOSITEID,j.PLOTID,j.HUMUSFORMCODE,j.DRAINAGECLASSCODE,j.STRATIFIED,j.EFFECTIVE_PORE_PATTERN,j.ELC_SUBSTRATE_TYPE,j.DEPTHTODISTINCTMOTTLES,j.DEPTHTOPROMINENTMOTTLES,
j.DEPTHTOGLEY,j.DEPTHTOBEDROCK,j.DEPTHTOCARBONATES,j.MOISTURE_REGIME_DEPTH_CLASS,j.MOISTUREREGIMECODE,j.MODEOFDEPOSITIONCODE1,j.MODEOFDEPOSITIONCODE2,j.FUNCTIONALROOTINGDEPTH,
j.MAXIMUMROOTINGDEPTH,j.DEPTHTOROOTRESTRICTION,j.DEPTHTOWATERTABLE,j.DEPTHTOIMPASSABLECOARSEFRAGMENTS,j.PROBLEMATICSITE,j.DEPTHTOSEEPAGE,j.SOIL_PIT_PHOTO,j.PRI_ECO,
j.PRI_ECO_PCT,j.SEC_ECO,j.SEC_ECO_PCT,j.AZIMUTH,j.DISTANCE,j.SOIL_PIT_PHOTO1,j.SOIL_PIT_PHOTO2,j.SUBSTRATENOTE,j.POREPATTERNCODE,j.MINERALTEXTURECODE,j.DECOMPOSITIONCODE,
j.PITAZIMUTH,j.PITDISTANCE,j.SUBSTRATEDATE,j.SUBSTRATEPERSON, j.Created, j.LastModified,j.IsDeleted,j.ERRORCOUNT,
j.ERRORMSG 
    FROM OPENJSON (@fromjson)
     WITH (
    ECOSITEID nvarchar(50),
	PLOTID nvarchar(50),
	HUMUSFORMCODE int,
	DRAINAGECLASSCODE int,
	STRATIFIED nvarchar(10),
	EFFECTIVE_PORE_PATTERN nvarchar(10),
	ELC_SUBSTRATE_TYPE nvarchar(10),
	DEPTHTODISTINCTMOTTLES int,
	DEPTHTOPROMINENTMOTTLES int,
	DEPTHTOGLEY int,
	DEPTHTOBEDROCK int,
	DEPTHTOCARBONATES int,
	MOISTURE_REGIME_DEPTH_CLASS nvarchar(10),
	MOISTUREREGIMECODE nvarchar(10),
	MODEOFDEPOSITIONCODE1 nvarchar(10),
	MODEOFDEPOSITIONCODE2 nvarchar(10),
	FUNCTIONALROOTINGDEPTH int,
	MAXIMUMROOTINGDEPTH int,
	DEPTHTOROOTRESTRICTION int,
	DEPTHTOWATERTABLE int,
	DEPTHTOIMPASSABLECOARSEFRAGMENTS int,
	PROBLEMATICSITE nvarchar(10),
	DEPTHTOSEEPAGE int,
	SOIL_PIT_PHOTO nvarchar(10),
	PRI_ECO nvarchar(50),
	PRI_ECO_PCT int,
	SEC_ECO nvarchar(50),
	SEC_ECO_PCT int,
	AZIMUTH int,
	DISTANCE int,
	SOIL_PIT_PHOTO1 varbinary(1),
	SOIL_PIT_PHOTO2 varbinary(1),
	SUBSTRATENOTE nvarchar(max),
	POREPATTERNCODE int,
	MINERALTEXTURECODE nvarchar(50),
	DECOMPOSITIONCODE nvarchar(10),
	PITAZIMUTH int,
	PITDISTANCE float,
	SUBSTRATEDATE nvarchar(50),
	SUBSTRATEPERSON nvarchar(100),
	Created datetime2,
	LastModified datetime2,
	IsDeleted nvarchar(1),
	ERRORCOUNT int,
ERRORMSG nvarchar(MAX)
     ) j left join ECOSITE ON j.ECOSITEID = ECOSITE.ECOSITEID where ECOSITE.ECOSITEID is null
	 RETURN 1
  END TRY
  BEGIN CATCH
     RETURN 0
  END CATCH

END
GO
/****** Object:  StoredProcedure [dbo].[InsertPerson]    Script Date: 2022-05-30 6:43:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[InsertPerson](@fromjson nvarchar(max))
AS BEGIN

  BEGIN TRY
    insert into PERSON(PERSONID,PROJECTID,FIRSTNAME,LASTNAME, Created, LastModified,IsDeleted)
    select j.PERSONID,j.PROJECTID,j.FIRSTNAME,j.LASTNAME, j.Created, j.LastModified,j.IsDeleted
    FROM OPENJSON (@fromjson)
     WITH (
    PERSONID nvarchar(50),
	PROJECTID nvarchar(50),
	FIRSTNAME nvarchar(50),
	LASTNAME nvarchar(50),
	Created datetime2,
	LastModified datetime2,
	IsDeleted nvarchar(1)
     ) j left join PERSON ON j.PERSONID = PERSON.PERSONID where PERSON.PERSONID is null
	 RETURN 1
  END TRY
  BEGIN CATCH
     RETURN 0
  END CATCH

END
GO
/****** Object:  StoredProcedure [dbo].[InsertPhoto]    Script Date: 2022-05-30 6:43:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[InsertPhoto](@fromjson nvarchar(max))
AS BEGIN

  BEGIN TRY
    insert into PHOTO(PHOTOID,PLOTID,PHOTOTYPE,DESCRIPTION,PHOTONUMBER,FRAMENUMBER,AZIMUTH,DISTANCE, Created, LastModified,IsDeleted,ERRORCOUNT,ERRORMSG)
    select j.PHOTOID,j.PLOTID,j.PHOTOTYPE,j.DESCRIPTION,j.PHOTONUMBER,j.FRAMENUMBER,j.AZIMUTH,j.DISTANCE, j.Created, j.LastModified,j.IsDeleted,j.ERRORCOUNT,j.ERRORMSG 
    FROM OPENJSON (@fromjson)
     WITH (
    PHOTOID nvarchar(50),
	PLOTID nvarchar(50),
	PHOTOTYPE nvarchar(50),
	DESCRIPTION nvarchar(max),
	PHOTONUMBER int,
	FRAMENUMBER int,
	AZIMUTH int,
	DISTANCE float,
	Created datetime2,
	LastModified datetime2,
	IsDeleted nvarchar(1),
	ERRORCOUNT int,
ERRORMSG nvarchar(MAX)
     ) j left join PHOTO ON j.PHOTOID = PHOTO.PHOTOID where PHOTO.PHOTOID is null
	 RETURN 1
  END TRY
  BEGIN CATCH
     RETURN 0
  END CATCH

END
GO
/****** Object:  StoredProcedure [dbo].[InsertPlot]    Script Date: 2022-05-30 6:43:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[InsertPlot](@fromjson nvarchar(max))
AS BEGIN

  BEGIN TRY
    insert into PLOT(PLOTID,PROJECTID,VSNPLOTTYPECODE,VSNPLOTNAME,PLOTKEY,PLOTOVERVIEWDATE,MEASURETYPECODE,LEAD_SPP,GROWTHPLOTNUMBER,CROWN_CLOSURE,FIELD_CREW1,FIELD_CREW2,
FIELD_CREW3,FIELD_CREW4,FIELD_CREW5,FIELD_CREW6,DECLINATION,UTMZONE,EASTING,NORTHING,DATUM,PLOTOVERVIEWNOTE,NONSTANDARDTYPECODE,ACCESSCONDITIONCODE,
MEASUREYEAR,EXISTINGPLOTNAME,EXISTINGPLOTTYPECODE,DISTANCETARGETMOVED,AZIMUTHTARGETMOVED,MATURITYCLASSCODE1,MATURITYCLASSRATIONALE1,MATURITYCLASSCODE2,MATURITYCLASSRATIONALE2,
CANOPYSTRUCTURECODE1,CANOPYSTRUCTURECODE2,MAINCANOPYORIGINCODE1,MAINCANOPYORIGINCODE2,DISTURBANCECODE1,DISTURBANCECODE2,SITERANK,CROWNDAMAGECODE,VIGOURCODE,DAMAGEDESCRIPTION,
FORESTHEALTHDATE,FORESTHEALTHNOTE,FORESTHEALTHPERSON,FOLLOWUPREQUIRED,SMALLTREESHRUBAREA,SMALLTREESHRUBDATE,SMALLTREESHRUBNOTE,SMALLTREEPERSON,
UNDERSTORYVEGETATIONDATE,UNDERSTORYVEGETATIONAREA,UNDERSTORYVEGETATIONNOTE,UNDERSTORYVEGETATIONPERSON,UNDERSTORYCENSUSDATE,UNDERSTORYCENSUSNOTE,UNDERSTORYCENSUSPERSON,DOWNWOODYDEBRISDATE,DOWNWOODYDEBRISNOTE,
DOWNWOODYDEBRISPERSON,DEFORMITYDATE,DEFORMITYNOTE,DEFORMITYPERSON,STANDINFODATE,STANDINFONOTE,STANDINFOPERSON,PERCENTAFFECTED,PERCENTMORTALITY, LINELENGTH1, LINELENGTH2,AGENOTE,AGEPERSON,STEMMAPPINGDATE,
AGEDATE,STEMMAPPINGNOTE,STEMMAPPINGPERSON,
Created,LastModified,IsDeleted,ERRORCOUNT,
ERRORMSG )
    select j.PLOTID, j.PROJECTID, j.VSNPLOTTYPECODE, j.VSNPLOTNAME, j.PLOTKEY, j.PLOTOVERVIEWDATE, j.MEASURETYPECODE, j.LEAD_SPP, j.GROWTHPLOTNUMBER, j.CROWN_CLOSURE, j.FIELD_CREW1, j.FIELD_CREW2, j.FIELD_CREW3, j.FIELD_CREW4, j.FIELD_CREW5, j.FIELD_CREW6, j.DECLINATION, j.UTMZONE, j.EASTING, j.NORTHING, j.DATUM, j.PLOTOVERVIEWNOTE, j.NONSTANDARDTYPECODE, j.ACCESSCONDITIONCODE, j.MEASUREYEAR, j.EXISTINGPLOTNAME, j.EXISTINGPLOTTYPECODE, j.DISTANCETARGETMOVED, j.AZIMUTHTARGETMOVED, j.MATURITYCLASSCODE1, j.MATURITYCLASSRATIONALE1, j.MATURITYCLASSCODE2, j.MATURITYCLASSRATIONALE2, j.CANOPYSTRUCTURECODE1, j.CANOPYSTRUCTURECODE2, j.MAINCANOPYORIGINCODE1, j.MAINCANOPYORIGINCODE2, j.DISTURBANCECODE1, j.DISTURBANCECODE2, j.SITERANK, j.CROWNDAMAGECODE, j.VIGOURCODE, j.DAMAGEDESCRIPTION, j.FORESTHEALTHDATE, j.FORESTHEALTHNOTE, j.FORESTHEALTHPERSON, j.FOLLOWUPREQUIRED, j.SMALLTREESHRUBAREA, j.SMALLTREESHRUBDATE, j.SMALLTREESHRUBNOTE, j.SMALLTREEPERSON, j.UNDERSTORYVEGETATIONDATE, j.UNDERSTORYVEGETATIONAREA, j.UNDERSTORYVEGETATIONNOTE, j.UNDERSTORYVEGETATIONPERSON, j.UNDERSTORYCENSUSDATE, j.UNDERSTORYCENSUSNOTE, j.UNDERSTORYCENSUSPERSON, j.DOWNWOODYDEBRISDATE, j.DOWNWOODYDEBRISNOTE, j.DOWNWOODYDEBRISPERSON, j.DEFORMITYDATE, j.DEFORMITYNOTE, j.DEFORMITYPERSON, j.STANDINFODATE, j.STANDINFONOTE, j.STANDINFOPERSON, j.PERCENTAFFECTED, j.PERCENTMORTALITY, j. LINELENGTH1, j. LINELENGTH2, j.AGENOTE, j.AGEPERSON, j.STEMMAPPINGDATE, j.AGEDATE, j.STEMMAPPINGNOTE, j.STEMMAPPINGPERSON, j.Created, j.LastModified, j.IsDeleted,j.ERRORCOUNT,j.ERRORMSG
    FROM OPENJSON (@fromjson)
     WITH (
    PLOTID nvarchar(50),
	PROJECTID nvarchar(50),
	VSNPLOTTYPECODE nvarchar(10),
	VSNPLOTNAME nvarchar(20),
	PLOTKEY nvarchar(max),
	PLOTOVERVIEWDATE nvarchar(50),
	MEASURETYPECODE nvarchar(max),
	LEAD_SPP int,
	GROWTHPLOTNUMBER int,
	CROWN_CLOSURE int,
	FIELD_CREW1 nvarchar(100),
	FIELD_CREW2 nvarchar(100),
	FIELD_CREW3 nvarchar(100),
	FIELD_CREW4 nvarchar(100),
	FIELD_CREW5 nvarchar(100),
	FIELD_CREW6 nvarchar(100),
	DECLINATION int,
	UTMZONE int,
	EASTING float,
	NORTHING float,
	DATUM nvarchar(50),
	PLOTOVERVIEWNOTE nvarchar(max),
	NONSTANDARDTYPECODE int,
	ACCESSCONDITIONCODE int,
	MEASUREYEAR int,
	EXISTINGPLOTNAME nvarchar(50),
	EXISTINGPLOTTYPECODE nvarchar(50),
	DISTANCETARGETMOVED float,
	AZIMUTHTARGETMOVED int,
	MATURITYCLASSCODE1 nvarchar(50),
	MATURITYCLASSRATIONALE1 nvarchar(max),
	MATURITYCLASSCODE2 nvarchar(50),
	MATURITYCLASSRATIONALE2 nvarchar(max),
	CANOPYSTRUCTURECODE1 nvarchar(10),
	CANOPYSTRUCTURECODE2 nvarchar(10),
	MAINCANOPYORIGINCODE1 int,
	MAINCANOPYORIGINCODE2 int,
	DISTURBANCECODE1 int,
	DISTURBANCECODE2 int,
	SITERANK int,
	CROWNDAMAGECODE int,
	VIGOURCODE int,
	DAMAGEDESCRIPTION nvarchar(max),
	FORESTHEALTHDATE nvarchar(50),
	FORESTHEALTHNOTE nvarchar(max),
	FORESTHEALTHPERSON nvarchar(100),
	FOLLOWUPREQUIRED nvarchar(1),
	SMALLTREESHRUBAREA int,
	SMALLTREESHRUBDATE nvarchar(50),
	SMALLTREESHRUBNOTE nvarchar(max),
	SMALLTREEPERSON nvarchar(100),
	UNDERSTORYVEGETATIONDATE nvarchar(50),
	UNDERSTORYVEGETATIONAREA int,
	UNDERSTORYVEGETATIONNOTE nvarchar(max),
	UNDERSTORYVEGETATIONPERSON nvarchar(100),
	UNDERSTORYCENSUSDATE nvarchar(50),
	UNDERSTORYCENSUSNOTE nvarchar(max),
	UNDERSTORYCENSUSPERSON nvarchar(100),
	DOWNWOODYDEBRISDATE nvarchar(50),
	DOWNWOODYDEBRISNOTE nvarchar(max),
	DOWNWOODYDEBRISPERSON nvarchar(100),
	DEFORMITYDATE nvarchar(50),
	DEFORMITYNOTE nvarchar(max),
	DEFORMITYPERSON nvarchar(100),
	STANDINFODATE nvarchar(50),
	STANDINFONOTE nvarchar(max),
	STANDINFOPERSON nvarchar(100),
	PERCENTAFFECTED int,
	PERCENTMORTALITY int,
    LINELENGTH1 float,
	LINELENGTH2 float,
	AGENOTE  nvarchar(max),
	AGEPERSON  nvarchar(100),
	STEMMAPPINGDATE nvarchar(50),
    AGEDATE nvarchar(50),
	STEMMAPPINGNOTE nvarchar(max),
	STEMMAPPINGPERSON  nvarchar(100),
	Created datetime2,
	LastModified datetime2,
	IsDeleted nvarchar(1),
	ERRORCOUNT int,
ERRORMSG nvarchar(MAX)
     ) j left join PLOT ON j.PLOTID = PLOT.PLOTID where PLOT.PLOTID is null
	 RETURN 1
  END TRY
  BEGIN CATCH
     RETURN 0
  END CATCH

END
GO
/****** Object:  StoredProcedure [dbo].[InsertProject]    Script Date: 2022-05-30 6:43:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertProject](@fromjson nvarchar(max))
AS BEGIN

  BEGIN TRY
    insert into PROJECT(PROJECTID, NAME, DESCRIPTION, PROJECT_DATE, Created, LastModified)
    select PROJECTID, NAME, DESCRIPTION, REPLACE(PROJECT_DATE,'T',' ') as PROJECT_DATE, REPLACE(Created,'T',' ') as Created, REPLACE(LastModified,'T',' ') as LastModified
    FROM OPENJSON (@fromjson)
     WITH (
            PROJECTID nvarchar(max),
            NAME nvarchar(max),
            DESCRIPTION nvarchar(max),
            PROJECT_DATE nvarchar(max),
			Created datetime2,
			LastModified datetime2
     )
	 RETURN 1
  END TRY
  BEGIN CATCH
     RETURN 0
  END CATCH

END
GO
/****** Object:  StoredProcedure [dbo].[InsertSmalltree]    Script Date: 2022-05-30 6:43:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[InsertSmalltree](@fromjson nvarchar(max))
AS BEGIN

  BEGIN TRY
    insert into SMALLTREE(SMALLTREEID,PLOTID,SPECIESCODE,HT_CLASS1_COUNT,HT_CLASS2_COUNT,HT_CLASS3_COUNT,HT_CLASS4_COUNT,HT_CLASS5_COUNT,HT_CLASS6_COUNT,HT_CLASS7_COUNT,HT_CLASS8_COUNT, HEIGHT, [COUNT],
Created,LastModified,IsDeleted,ERRORCOUNT,
ERRORMSG)
    select j.SMALLTREEID,j.PLOTID,j.SPECIESCODE,j.HT_CLASS1_COUNT,j.HT_CLASS2_COUNT,j.HT_CLASS3_COUNT,j.HT_CLASS4_COUNT,j.HT_CLASS5_COUNT,j.HT_CLASS6_COUNT,j.HT_CLASS7_COUNT,j.HT_CLASS8_COUNT,j.HEIGHT, j.[COUNT],j.Created,j.LastModified,j.IsDeleted,j.ERRORCOUNT,j.ERRORMSG
    FROM OPENJSON (@fromjson)
     WITH (
  SMALLTREEID nvarchar(50),
PLOTID nvarchar(50),
SPECIESCODE int,
HT_CLASS1_COUNT int,
HT_CLASS2_COUNT int,
HT_CLASS3_COUNT int,
HT_CLASS4_COUNT int,
HT_CLASS5_COUNT int,
HT_CLASS6_COUNT int,
HT_CLASS7_COUNT int,
HT_CLASS8_COUNT int,
Created datetime2,
LastModified datetime2,
IsDeleted nvarchar(1),
HEIGHT float,
[COUNT] int,
ERRORCOUNT int,
ERRORMSG nvarchar(MAX)
     ) j left join SMALLTREE ON j.SMALLTREEID = SMALLTREE.SMALLTREEID where SMALLTREE.SMALLTREEID is null
	 RETURN 1
  END TRY
  BEGIN CATCH
     RETURN 0
  END CATCH

END
GO
/****** Object:  StoredProcedure [dbo].[InsertSmalltreeTally]    Script Date: 2022-05-30 6:43:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[InsertSmalltreeTally](@fromjson nvarchar(max))
AS BEGIN

  BEGIN TRY
    insert into SMALLTREETALLY(SMALLTREETALLYID,PLOTID,SPECIESCODE, DBH, HEIGHT, [COUNT], Created,LastModified,IsDeleted, ERRORCOUNT, ERRORMSG)
    select j.SMALLTREETALLYID,j.PLOTID,j.SPECIESCODE,j.DBH, j.HEIGHT, j.[COUNT],j.Created,j.LastModified,j.IsDeleted, j.ERRORCOUNT, j.ERRORMSG
    FROM OPENJSON (@fromjson)
     WITH (
  SMALLTREETALLYID nvarchar(50),
PLOTID nvarchar(50),
SPECIESCODE int,
DBH float,
HEIGHT float,
[COUNT] int,
Created datetime2,
LastModified datetime2,
IsDeleted nvarchar(1),
ERRORCOUNT int,
ERRORMSG nvarchar(MAX)

     ) j left join SMALLTREETALLY ON j.SMALLTREETAllYID = SMALLTREETALLY.SMALLTREETALLYID where SMALLTREETALLY.SMALLTREETALLYID is null
	 RETURN 1
  END TRY
  BEGIN CATCH
     RETURN 0
  END CATCH

END
GO
/****** Object:  StoredProcedure [dbo].[InsertSoil]    Script Date: 2022-05-30 6:43:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[InsertSoil](@fromjson nvarchar(max))
AS BEGIN

  BEGIN TRY
    insert into SOIL(SOILID,PLOTID,HORIZONNUMBER,DEPTHFROM,DEPTHTO,HORIZON,DECOMPOSITIONCODE,MINERALTEXTURECODE,POREPATTERNCODE,STRUCTURE,MATRIXCOLOUR,
MOTTLECOLOUR,PERCENTGRAVEL,PERCENTCOBBLE,PERCENTSTONE,GLEYCOLOUR,Created,LastModified,IsDeleted,ERRORCOUNT,ERRORMSG)
    select j.SOILID,j.PLOTID,j.HORIZONNUMBER,j.DEPTHFROM,j.DEPTHTO,j.HORIZON,j.DECOMPOSITIONCODE,j.MINERALTEXTURECODE,j.POREPATTERNCODE,j.STRUCTURE,j.MATRIXCOLOUR,
j.MOTTLECOLOUR,j.PERCENTGRAVEL,j.PERCENTCOBBLE,j.PERCENTSTONE,j.GLEYCOLOUR,j.Created,j.LastModified,j.IsDeleted,j.ERRORCOUNT,
j.ERRORMSG
    FROM OPENJSON (@fromjson)
     WITH (
SOILID nvarchar(50),
PLOTID nvarchar(50),
HORIZONNUMBER int,
[DEPTHFROM] float,
[DEPTHTO] float,
HORIZON nvarchar(50),
DECOMPOSITIONCODE nvarchar(50),
MINERALTEXTURECODE nvarchar(50),
POREPATTERNCODE nvarchar(50),
STRUCTURE nvarchar(50),
MATRIXCOLOUR nvarchar(50),
MOTTLECOLOUR nvarchar(50),
PERCENTGRAVEL int,
PERCENTCOBBLE int,
PERCENTSTONE int,
GLEYCOLOUR nvarchar(50),
Created datetime2,
LastModified datetime2,
IsDeleted nvarchar(1),
ERRORCOUNT int,
ERRORMSG nvarchar(MAX)
     ) j left join SOIL ON j.SOILID = SOIL.SOILID where SOIL.SOILID is null
	 RETURN 1
  END TRY
  BEGIN CATCH
     RETURN 0
  END CATCH

END
GO
/****** Object:  StoredProcedure [dbo].[InsertStemmap]    Script Date: 2022-05-30 6:43:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[InsertStemmap](@fromjson nvarchar(max))
AS BEGIN

  BEGIN TRY
    insert into STEMMAP(STEMMAPID,TREEID,AZIMUTH,DISTANCE,CROWNWIDTH1,CROWNWIDTH2,CROWNOFFSETAZIMUTH,CROWNOFFSETDISTANCE,Created,LastModified,IsDeleted,ERRORCOUNT,
ERRORMSG)
    select j.STEMMAPID,j.TREEID,j.AZIMUTH,j.DISTANCE,j.CROWNWIDTH1,j.CROWNWIDTH2,j.CROWNOFFSETAZIMUTH,j.CROWNOFFSETDISTANCE,j.Created,j.LastModified,j.IsDeleted,j.ERRORCOUNT,
j.ERRORMSG
    FROM OPENJSON (@fromjson)
     WITH (
STEMMAPID nvarchar(50),
TREEID nvarchar(50),
AZIMUTH int,
DISTANCE float,
CROWNWIDTH1 float,
CROWNWIDTH2 float,
CROWNOFFSETAZIMUTH int,
CROWNOFFSETDISTANCE float,
Created datetime2,
LastModified datetime2,
IsDeleted nvarchar(1),
ERRORCOUNT int,
ERRORMSG nvarchar(MAX)
     ) j left join STEMMAP ON j.STEMMAPID = STEMMAP.STEMMAPID where STEMMAP.STEMMAPID is null
	 RETURN 1
  END TRY
  BEGIN CATCH
     RETURN 0
  END CATCH

END
GO
/****** Object:  StoredProcedure [dbo].[InsertTree]    Script Date: 2022-05-30 6:43:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[InsertTree](@fromjson nvarchar(max))
AS BEGIN

  BEGIN TRY
    insert into TREE(TREEID,PLOTID,SECTION,TREENUMBER,SPECIESCODE,TAG_TYPE,TREEORIGINCODE,TREESTATUSCODE,VIGOURCODE,HEIGHTTODBH,DBH,HT,
[LENGTH],DBHIN,CROWNIN,LIVE_CROWN_RATIO,CROWNCLASSCODE,CROWN_POSITION,CROWNDAMAGECODE,DEFOLIATING_INSECT,FOLIAR_DISEASE,STEMQUALITYCODE,BARKRETENTIONCODE,
WOODCONDITIONCODE,DECAYCLASS,MORTALITYCAUSECODE,BROKENTOP,AZIMUTH,DISTANCE,CROWNWIDTH1,CROWNWIDTH2,COMMENTS,DBH1,
DBH2,DIRECTTOTALHEIGHT,OCULARTOTALHEIGHT,HEIGHTTODEADTIP,DIRECTHEIGHTTOCONTLIVECROWN,OCULARHEIGHTTOCONTLIVECROWN,
DIRECTOFFSETDISTANCE,DEGREEOFLEAN,HEIGHTTOCORE,CORESTATUSCODE,SOUNDWOODLENGTH,FIELDAGE,OFFICERINGCOUNT,MISSINGRINGS,VSNSTATUSCODE,
Created,LastModified,IsDeleted,ERRORCOUNT,ERRORMSG)
    select           j.TREEID,j.PLOTID,j.SECTION,j.TREENUMBER,j.SPECIESCODE,j.TAG_TYPE,j.TREEORIGINCODE,j.TREESTATUSCODE,j.VIGOURCODE,j.HEIGHTTODBH,j.DBH,j.HT,
j.[LENGTH],j.DBHIN,j.CROWNIN,j.LIVE_CROWN_RATIO,j.CROWNCLASSCODE,j.CROWN_POSITION,j.CROWNDAMAGECODE,j.DEFOLIATING_INSECT,j.FOLIAR_DISEASE,j.STEMQUALITYCODE,j.BARKRETENTIONCODE,
j.WOODCONDITIONCODE,j.DECAYCLASS,j.MORTALITYCAUSECODE,j.BROKENTOP,j.AZIMUTH,j.DISTANCE,j.CROWNWIDTH1,j.CROWNWIDTH2,j.COMMENTS,j.DBH1,
j.DBH2,j.DIRECTTOTALHEIGHT,j.OCULARTOTALHEIGHT,j.HEIGHTTODEADTIP,j.DIRECTHEIGHTTOCONTLIVECROWN,j.OCULARHEIGHTTOCONTLIVECROWN,
j.DIRECTOFFSETDISTANCE,j.DEGREEOFLEAN,j.HEIGHTTOCORE,j.CORESTATUSCODE,j.SOUNDWOODLENGTH,j.FIELDAGE,j.OFFICERINGCOUNT,j.MISSINGRINGS,j.VSNSTATUSCODE,
j.Created,j.LastModified,j.IsDeleted,j.ERRORCOUNT,j.ERRORMSG
    FROM OPENJSON (@fromjson)
     WITH (
TREEID nvarchar(50),
PLOTID nvarchar(50),
SECTION int,
TREENUMBER int,
SPECIESCODE int,
TAG_TYPE nvarchar(50),
TREEORIGINCODE nvarchar(10),
TREESTATUSCODE nvarchar(10),
VIGOURCODE int,
HEIGHTTODBH float,
DBH float,
HT float,
[LENGTH] float,
DBHIN nvarchar(1),
CROWNIN nvarchar(1),
LIVE_CROWN_RATIO int,
CROWNCLASSCODE nvarchar(10),
CROWN_POSITION int,
CROWNDAMAGECODE int,
DEFOLIATING_INSECT int,
FOLIAR_DISEASE int,
STEMQUALITYCODE nvarchar(10),
BARKRETENTIONCODE int,
WOODCONDITIONCODE int,
DECAYCLASS int,
MORTALITYCAUSECODE int,
BROKENTOP nvarchar(1),
AZIMUTH float,
DISTANCE float,
CROWNWIDTH1 float,
CROWNWIDTH2 float,
COMMENTS nvarchar(max),
DBH1 float,
DBH2	float,
DIRECTTOTALHEIGHT	float,
OCULARTOTALHEIGHT	float,
HEIGHTTODEADTIP	float,
DIRECTHEIGHTTOCONTLIVECROWN	float,
OCULARHEIGHTTOCONTLIVECROWN	float,
DIRECTOFFSETDISTANCE	float,
DEGREEOFLEAN	int,
HEIGHTTOCORE	float,
CORESTATUSCODE	nvarchar(10),
SOUNDWOODLENGTH	float,
FIELDAGE	int,
OFFICERINGCOUNT	int,
MISSINGRINGS	int,
VSNSTATUSCODE nvarchar(10),
Created datetime2,
LastModified datetime2,
IsDeleted nvarchar(1),
ERRORCOUNT int,
ERRORMSG nvarchar(MAX)
     ) j left join TREE ON j.TREEID = TREE.TREEID where TREE.TREEID is null
	 RETURN 1
  END TRY
  BEGIN CATCH
     RETURN ERROR_MESSAGE()
  END CATCH

END
GO
/****** Object:  StoredProcedure [dbo].[InsertVegetation]    Script Date: 2022-05-30 6:43:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[InsertVegetation](@fromjson nvarchar(max))
AS BEGIN

  BEGIN TRY
    insert into VEGETATION(VEGETATIONID,PLOTID,VSNSPECIESCODE,SPECIMENNUMBER,QUAD1,QUAD2,QUAD3,QUAD4,ELCLAYER3,ELCLAYER4,ELCLAYER5,ELCLAYER6,ELCLAYER7,Created,LastModified,IsDeleted,
	QUAD1_ELC4,QUAD2_ELC4,QUAD3_ELC4,QUAD4_ELC4,
	QUAD1_ELC5,QUAD2_ELC5,QUAD3_ELC5,QUAD4_ELC5,
	QUAD1_ELC6,QUAD2_ELC6,QUAD3_ELC6,QUAD4_ELC6,
	QUAD1_ELC7,QUAD2_ELC7,QUAD3_ELC7,QUAD4_ELC7,ERRORCOUNT,ERRORMSG)

    select j.VEGETATIONID, j.PLOTID, j.VSNSPECIESCODE, j.SPECIMENNUMBER, j.QUAD1, j.QUAD2, j.QUAD3, j.QUAD4, j.ELCLAYER3, j.ELCLAYER4, j.ELCLAYER5, j.ELCLAYER6, j.ELCLAYER7, j.Created, j.LastModified, j.IsDeleted, j.QUAD1_ELC4, j.QUAD2_ELC4, j.QUAD3_ELC4, j.QUAD4_ELC4, j.QUAD1_ELC5, j.QUAD2_ELC5, j.QUAD3_ELC5, j.QUAD4_ELC5, j.QUAD1_ELC6, j.QUAD2_ELC6, j.QUAD3_ELC6, j.QUAD4_ELC6, j.QUAD1_ELC7, j.QUAD2_ELC7, j.QUAD3_ELC7, j.QUAD4_ELC7,j.ERRORCOUNT,j.ERRORMSG
    FROM OPENJSON (@fromjson)
     WITH (
VEGETATIONID nvarchar(50),
PLOTID nvarchar(50),
VSNSPECIESCODE nvarchar(50),
SPECIMENNUMBER int,
QUAD1 float,
QUAD2 float,
QUAD3 float,
QUAD4 float,
ELCLAYER3 float,
ELCLAYER4 float,
ELCLAYER5 float,
ELCLAYER6 float,
ELCLAYER7 float,
Created datetime2,
LastModified datetime2,
IsDeleted nvarchar(1),
QUAD1_ELC4 float,
QUAD2_ELC4 float,
QUAD3_ELC4 float,
QUAD4_ELC4 float,
QUAD1_ELC5 float,
QUAD2_ELC5 float,
QUAD3_ELC5 float,
QUAD4_ELC5 float,
QUAD1_ELC6 float,
QUAD2_ELC6 float,
QUAD3_ELC6 float,
QUAD4_ELC6 float,
QUAD1_ELC7 float,
QUAD2_ELC7 float,
QUAD3_ELC7 float,
QUAD4_ELC7 float,
ERRORCOUNT int,
ERRORMSG nvarchar(MAX)

)  j left join VEGETATION ON j.VEGETATIONID = VEGETATION.VEGETATIONID where VEGETATION.VEGETATIONID is null
	 RETURN 1
  END TRY
  BEGIN CATCH
     RETURN 0
  END CATCH

END


/****** Object:  StoredProcedure [dbo].[UpdateVegetation]    Script Date: 6/21/2021 12:53:37 PM ******/
SET ANSI_NULLS ON
GO
/****** Object:  StoredProcedure [dbo].[InsertVegetationCensus]    Script Date: 2022-05-30 6:43:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[InsertVegetationCensus](@fromjson nvarchar(max))
AS BEGIN

  BEGIN TRY
    insert into VEGETATIONCENSUS(VEGETATIONCENSUSID,PLOTID,VSNSPECIESCODE,SPECIMENNUMBER,Created,LastModified,IsDeleted,ERRORCOUNT,ERRORMSG)
    select j.VEGETATIONCENSUSID,j.PLOTID,j.VSNSPECIESCODE,j.SPECIMENNUMBER,j.Created,j.LastModified,j.IsDeleted,j.ERRORCOUNT,j.ERRORMSG
    FROM OPENJSON (@fromjson)
     WITH (
VEGETATIONCENSUSID nvarchar(50),
PLOTID nvarchar(50),
VSNSPECIESCODE nvarchar(50),
SPECIMENNUMBER int,
Created datetime2,
LastModified datetime2,
IsDeleted nvarchar(1),
ERRORCOUNT int,
ERRORMSG nvarchar(MAX)
) j left join VEGETATIONCENSUS ON j.VEGETATIONCENSUSID = VEGETATIONCENSUS.VEGETATIONCENSUSID where VEGETATIONCENSUS.VEGETATIONCENSUSID is null
	 RETURN 1
  END TRY
  BEGIN CATCH
     RETURN 0
  END CATCH

END
GO
/****** Object:  StoredProcedure [dbo].[UpdateDeformity]    Script Date: 2022-05-30 6:43:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/****** Object:  StoredProcedure [dbo].[InsertProject]    Script Date: 4/13/2020 2:58:46 PM ******/

CREATE PROCEDURE [dbo].[UpdateDeformity](@fromjson nvarchar(max))
AS BEGIN

  BEGIN TRY
 
    with json_data as (select DEFORMITYID, TREEID, DEFORMITYTYPECODE, CAUSE,HEIGHTFROM,HEIGHTTO, QUADRANTCODE,EXTENT,DEGREELEANARCH,AZIMUTH,DEFORMITYLENGTH,
DEFORMITYWIDTH,SCUFF,SCRAPE,GOUGE,OLD_FEEDING_CAVITY,NEW_FEEDING_CAVITY,OLD_NEST_CAVITY,NEW_NEST_CAVITY,STICK_NEST,Created,LastModified,IsDeleted,ERRORCOUNT,
ERRORMSG
    FROM OPENJSON (@fromjson) 
    WITH (
DEFORMITYID nvarchar(50),
TREEID nvarchar(50),
DEFORMITYTYPECODE int,
CAUSE int,
HEIGHTFROM float,
HEIGHTTO float,
QUADRANTCODE nvarchar(10),
EXTENT int,
DEGREELEANARCH int,
AZIMUTH int,
DEFORMITYLENGTH float,
DEFORMITYWIDTH float,
SCUFF int,
SCRAPE int,
GOUGE int,
OLD_FEEDING_CAVITY nvarchar(1),
NEW_FEEDING_CAVITY nvarchar(1),
OLD_NEST_CAVITY nvarchar(1),
NEW_NEST_CAVITY nvarchar(1),
STICK_NEST nvarchar(1),
Created datetime2,
LastModified datetime2,
IsDeleted nvarchar(1),
ERRORCOUNT int,
ERRORMSG nvarchar(MAX)
    ))
    update u SET 
u.DEFORMITYID=jd.DEFORMITYID,
u.TREEID=jd.TREEID,
u.DEFORMITYTYPECODE=jd.DEFORMITYTYPECODE,
u.CAUSE=jd.CAUSE,
u.HEIGHTFROM=jd.HEIGHTFROM,
u.HEIGHTTO=jd.HEIGHTTO,
u.QUADRANTCODE=jd.QUADRANTCODE,
u.EXTENT=jd.EXTENT,
u.DEGREELEANARCH=jd.DEGREELEANARCH,
u.AZIMUTH=jd.AZIMUTH,
u.DEFORMITYLENGTH=jd.DEFORMITYLENGTH,
u.DEFORMITYWIDTH=jd.DEFORMITYWIDTH,
u.SCUFF=jd.SCUFF,
u.SCRAPE=jd.SCRAPE,
u.GOUGE=jd.GOUGE,
u.OLD_FEEDING_CAVITY=jd.OLD_FEEDING_CAVITY,
u.NEW_FEEDING_CAVITY=jd.NEW_FEEDING_CAVITY,
u.OLD_NEST_CAVITY=jd.OLD_NEST_CAVITY,
u.NEW_NEST_CAVITY=jd.NEW_NEST_CAVITY,
u.STICK_NEST=jd.STICK_NEST,
u.Created=jd.Created,
u.LastModified=jd.LastModified,
u.IsDeleted=jd.IsDeleted,
u.LastModifiedAtServer = CURRENT_TIMESTAMP,
u.ERRORCOUNT=jd.ERRORCOUNT,
u.ERRORMSG=jd.ERRORMSG
	from DEFORMITY as u
	inner join json_data as jd on jd.DEFORMITYID = u.DEFORMITYID
    WHERE
	dbo.IsPlotCompletefromTreeID(jd.TREEID) = 0
    RETURN 1
  END TRY
  BEGIN CATCH
     RETURN 0
  END CATCH

END
GO
/****** Object:  StoredProcedure [dbo].[UpdateDWD]    Script Date: 2022-05-30 6:43:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/****** Object:  StoredProcedure [dbo].[InsertProject]    Script Date: 4/13/2020 2:58:46 PM ******/

CREATE PROCEDURE [dbo].[UpdateDWD](@fromjson nvarchar(max))
AS BEGIN

  BEGIN TRY
 
    with json_data as (select DWDID,PLOTID,LINENUMBER,DWDNUM,SPECIESCODE,SMALLDIAMETER,DECOMPOSITIONCLASS,DOWNWOODYDEBRISORIGINCODE,TILTANGLE,DOWNWOODYDEBRISLENGTH,DIAMETER,LARGEDIAMETER,MOSS,BURNED,HOLLOW,IS_ACCUM,
ACCUMULATIONLENGTH,ACCUMULATIONDEPTH,PERCENTCONIFER,PERCENTHARDWOOD, Created, LastModified, IsDeleted ,ERRORCOUNT,ERRORMSG
    FROM OPENJSON (@fromjson) 
    WITH (
  DWDID nvarchar(50),
	PLOTID nvarchar(50) ,
	LINENUMBER int,
	DWDNUM int,
	SPECIESCODE int,
	SMALLDIAMETER float,
	DECOMPOSITIONCLASS int,
	DOWNWOODYDEBRISORIGINCODE nvarchar(10),
	TILTANGLE int,
	[DOWNWOODYDEBRISLENGTH] float,
	DIAMETER float,
	LARGEDIAMETER float,
	MOSS nvarchar(1),
	BURNED nvarchar(1),
	HOLLOW nvarchar(1),
	IS_ACCUM nvarchar(1),
	ACCUMULATIONLENGTH float,
	ACCUMULATIONDEPTH float,
	PERCENTCONIFER int,
	PERCENTHARDWOOD int,
	Created datetime2,
	LastModified datetime2,
	IsDeleted nvarchar(1),
	ERRORCOUNT int,
    ERRORMSG nvarchar(MAX)
    ))
    update u SET 
	u.DWDID=jd.DWDID,
u.PLOTID=jd.PLOTID,
u.LINENUMBER=jd.LINENUMBER,
u.DWDNUM=jd.DWDNUM,
u.SPECIESCODE=jd.SPECIESCODE,
u.SMALLDIAMETER=jd.SMALLDIAMETER,
u.DECOMPOSITIONCLASS=jd.DECOMPOSITIONCLASS,
u.DOWNWOODYDEBRISORIGINCODE=jd.DOWNWOODYDEBRISORIGINCODE,
u.TILTANGLE=jd.TILTANGLE,
u.DOWNWOODYDEBRISLENGTH=jd.DOWNWOODYDEBRISLENGTH,
u.DIAMETER=jd.DIAMETER,
u.LARGEDIAMETER=jd.LARGEDIAMETER,
u.MOSS=jd.MOSS,
u.BURNED=jd.BURNED,
u.HOLLOW=jd.HOLLOW,
u.IS_ACCUM=jd.IS_ACCUM,
u.ACCUMULATIONLENGTH=jd.ACCUMULATIONLENGTH,
u.ACCUMULATIONDEPTH=jd.ACCUMULATIONDEPTH,
u.PERCENTCONIFER=jd.PERCENTCONIFER,
u.PERCENTHARDWOOD=jd.PERCENTHARDWOOD,
u.Created=jd.Created,
u.LastModified=jd.LastModified,
u.IsDeleted=jd.IsDeleted,
u.LastModifiedAtServer = CURRENT_TIMESTAMP,
u.ERRORCOUNT = jd.ERRORCOUNT,
u.ERRORMSG = jd.ERRORMSG
	from DWD as u
	inner join json_data as jd on jd.DWDID = u.DWDID
   WHERE
	dbo.IsPlotCompletefromPlotID(jd.PLOTID) = 0
    RETURN 1
  END TRY
  BEGIN CATCH
     RETURN 0
  END CATCH

END
GO
/****** Object:  StoredProcedure [dbo].[UpdateEcosite]    Script Date: 2022-05-30 6:43:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/****** Object:  StoredProcedure [dbo].[InsertProject]    Script Date: 4/13/2020 2:58:46 PM ******/

CREATE PROCEDURE [dbo].[UpdateEcosite](@fromjson nvarchar(max))
AS BEGIN

  BEGIN TRY
 
    with json_data as (select ECOSITEID,PLOTID,HUMUSFORMCODE,DRAINAGECLASSCODE,STRATIFIED,EFFECTIVE_PORE_PATTERN,ELC_SUBSTRATE_TYPE,DEPTHTODISTINCTMOTTLES,DEPTHTOPROMINENTMOTTLES,
DEPTHTOGLEY,DEPTHTOBEDROCK,DEPTHTOCARBONATES,MOISTURE_REGIME_DEPTH_CLASS,MOISTUREREGIMECODE,MODEOFDEPOSITIONCODE1,MODEOFDEPOSITIONCODE2,FUNCTIONALROOTINGDEPTH,
MAXIMUMROOTINGDEPTH,DEPTHTOROOTRESTRICTION,DEPTHTOWATERTABLE,DEPTHTOIMPASSABLECOARSEFRAGMENTS,PROBLEMATICSITE,DEPTHTOSEEPAGE,SOIL_PIT_PHOTO,PRI_ECO,
PRI_ECO_PCT,SEC_ECO,SEC_ECO_PCT,AZIMUTH,DISTANCE,SOIL_PIT_PHOTO1,SOIL_PIT_PHOTO2,SUBSTRATENOTE,POREPATTERNCODE,MINERALTEXTURECODE,DECOMPOSITIONCODE,
PITAZIMUTH,PITDISTANCE,SUBSTRATEDATE,SUBSTRATEPERSON, Created, LastModified,IsDeleted,ERRORCOUNT,ERRORMSG
    FROM OPENJSON (@fromjson) 
    WITH (
  ECOSITEID nvarchar(50),
	PLOTID nvarchar(50),
	HUMUSFORMCODE int,
	DRAINAGECLASSCODE int,
	STRATIFIED nvarchar(10),
	EFFECTIVE_PORE_PATTERN nvarchar(10),
	ELC_SUBSTRATE_TYPE nvarchar(10),
	DEPTHTODISTINCTMOTTLES int,
	DEPTHTOPROMINENTMOTTLES int,
	DEPTHTOGLEY int,
	DEPTHTOBEDROCK int,
	DEPTHTOCARBONATES int,
	MOISTURE_REGIME_DEPTH_CLASS nvarchar(10),
	MOISTUREREGIMECODE nvarchar(10),
	MODEOFDEPOSITIONCODE1 nvarchar(10),
	MODEOFDEPOSITIONCODE2 nvarchar(10),
	FUNCTIONALROOTINGDEPTH int,
	MAXIMUMROOTINGDEPTH int,
	DEPTHTOROOTRESTRICTION int,
	DEPTHTOWATERTABLE int,
	DEPTHTOIMPASSABLECOARSEFRAGMENTS int,
	PROBLEMATICSITE nvarchar(10),
	DEPTHTOSEEPAGE int,
	SOIL_PIT_PHOTO nvarchar(10),
	PRI_ECO nvarchar(50),
	PRI_ECO_PCT int,
	SEC_ECO nvarchar(50),
	SEC_ECO_PCT int,
	AZIMUTH int,
	DISTANCE int,
	SOIL_PIT_PHOTO1 varbinary(1),
	SOIL_PIT_PHOTO2 varbinary(1),
	SUBSTRATENOTE nvarchar(max),
	POREPATTERNCODE int,
	MINERALTEXTURECODE nvarchar(50),
	DECOMPOSITIONCODE nvarchar(10),
	PITAZIMUTH int,
	PITDISTANCE float,
	SUBSTRATEDATE nvarchar(50),
	SUBSTRATEPERSON nvarchar(100),
	Created datetime2,
	LastModified datetime2,
	IsDeleted nvarchar(1),
	ERRORCOUNT int,
    ERRORMSG nvarchar(MAX)
    ))
    update u SET 
u.ECOSITEID=jd.ECOSITEID,
u.PLOTID=jd.PLOTID,
u.HUMUSFORMCODE=jd.HUMUSFORMCODE,
u.DRAINAGECLASSCODE=jd.DRAINAGECLASSCODE,
u.STRATIFIED =jd.STRATIFIED ,
u.EFFECTIVE_PORE_PATTERN=jd.EFFECTIVE_PORE_PATTERN,
u.ELC_SUBSTRATE_TYPE=jd.ELC_SUBSTRATE_TYPE,
u.DEPTHTODISTINCTMOTTLES=jd.DEPTHTODISTINCTMOTTLES,
u.DEPTHTOPROMINENTMOTTLES=jd.DEPTHTOPROMINENTMOTTLES,
u.DEPTHTOGLEY=jd.DEPTHTOGLEY,
u.DEPTHTOBEDROCK=jd.DEPTHTOBEDROCK,
u.DEPTHTOCARBONATES=jd.DEPTHTOCARBONATES,
u.MOISTURE_REGIME_DEPTH_CLASS=jd.MOISTURE_REGIME_DEPTH_CLASS,
u.MOISTUREREGIMECODE=jd.MOISTUREREGIMECODE,
u.MODEOFDEPOSITIONCODE1=jd.MODEOFDEPOSITIONCODE1,
u.MODEOFDEPOSITIONCODE2=jd.MODEOFDEPOSITIONCODE2,
u.FUNCTIONALROOTINGDEPTH=jd.FUNCTIONALROOTINGDEPTH,
u.MAXIMUMROOTINGDEPTH=jd.MAXIMUMROOTINGDEPTH,
u.DEPTHTOROOTRESTRICTION=jd.DEPTHTOROOTRESTRICTION,
u.DEPTHTOWATERTABLE=jd.DEPTHTOWATERTABLE,
u.DEPTHTOIMPASSABLECOARSEFRAGMENTS=jd.DEPTHTOIMPASSABLECOARSEFRAGMENTS,
u.PROBLEMATICSITE=jd.PROBLEMATICSITE,
u.DEPTHTOSEEPAGE=jd.DEPTHTOSEEPAGE,
u.SOIL_PIT_PHOTO=jd.SOIL_PIT_PHOTO,
u.PRI_ECO=jd.PRI_ECO,
u.PRI_ECO_PCT=jd.PRI_ECO_PCT,
u.SEC_ECO=jd.SEC_ECO,
u.SEC_ECO_PCT=jd.SEC_ECO_PCT,
u.AZIMUTH=jd.AZIMUTH,
u.DISTANCE=jd.DISTANCE,
u.SOIL_PIT_PHOTO1=jd.SOIL_PIT_PHOTO1,
u.SOIL_PIT_PHOTO2=jd.SOIL_PIT_PHOTO2,
u.SUBSTRATENOTE=jd.SUBSTRATENOTE,
u.POREPATTERNCODE=jd.POREPATTERNCODE,
u.MINERALTEXTURECODE=jd.MINERALTEXTURECODE,
u.DECOMPOSITIONCODE=jd.DECOMPOSITIONCODE,
u.PITAZIMUTH=jd.PITAZIMUTH,
u.PITDISTANCE=jd.PITDISTANCE,
u.SUBSTRATEDATE=jd.SUBSTRATEDATE,
u.SUBSTRATEPERSON=jd.SUBSTRATEPERSON,
u.Created=jd.Created,
u.LastModified=jd.LastModified,
u.IsDeleted=jd.IsDeleted,
u.LastModifiedAtServer = CURRENT_TIMESTAMP,
u.ERRORCOUNT = jd.ERRORCOUNT,
u.ERRORMSG = jd.ERRORMSG
	from ECOSITE as u
	inner join json_data as jd on jd.ECOSITEID = u.ECOSITEID
   WHERE
	dbo.IsPlotCompletefromPlotID(jd.PLOTID) = 0
    RETURN 1
  END TRY
  BEGIN CATCH
     RETURN 0
  END CATCH

END
GO
/****** Object:  StoredProcedure [dbo].[UpdatePerson]    Script Date: 2022-05-30 6:43:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/****** Object:  StoredProcedure [dbo].[InsertPerson]    Script Date: 4/13/2020 2:58:46 PM ******/

CREATE PROCEDURE [dbo].[UpdatePerson](@fromjson nvarchar(max))
AS BEGIN

  BEGIN TRY
 
    with json_data as (select PERSONID,PROJECTID,FIRSTNAME,LASTNAME, Created, LastModified, IsDeleted 
    FROM OPENJSON (@fromjson) 
    WITH (
    PERSONID nvarchar(50),
	PROJECTID nvarchar(50),
	FIRSTNAME nvarchar(50),
	LASTNAME nvarchar(50),
	Created datetime2,
	LastModified datetime2,
	IsDeleted nvarchar(1)
    ))
    update u SET 
	u.PERSONID=jd.PERSONID,
u.PROJECTID=jd.PROJECTID,
u.FIRSTNAME=jd.FIRSTNAME,
u.LASTNAME=jd.LASTNAME,
u.Created=jd.Created,
u.LastModified=jd.LastModified,
u.IsDeleted=jd.IsDeleted,
	u.LastModifiedAtServer = CURRENT_TIMESTAMP
	from PERSON as u
	inner join json_data as jd on jd.PERSONID = u.PERSONID
   
    RETURN 1
  END TRY
  BEGIN CATCH
     RETURN 0
  END CATCH

END
GO
/****** Object:  StoredProcedure [dbo].[UpdatePhoto]    Script Date: 2022-05-30 6:43:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/****** Object:  StoredProcedure [dbo].[InsertPhoto]    Script Date: 4/13/2020 2:58:46 PM ******/

CREATE PROCEDURE [dbo].[UpdatePhoto](@fromjson nvarchar(max))
AS BEGIN

  BEGIN TRY
 
    with json_data as (select PHOTOID,PLOTID,PHOTOTYPE,DESCRIPTION,PHOTONUMBER,FRAMENUMBER,AZIMUTH,DISTANCE,Created, LastModified, IsDeleted ,ERRORCOUNT,ERRORMSG
    FROM OPENJSON (@fromjson) 
    WITH (
    PHOTOID nvarchar(50),
	PLOTID nvarchar(50),
	PHOTOTYPE nvarchar(50),
	DESCRIPTION nvarchar(max),
	PHOTONUMBER int,
	FRAMENUMBER int,
	AZIMUTH int,
	DISTANCE float,
	Created datetime2,
	LastModified datetime2,
	IsDeleted nvarchar(1),
	ERRORCOUNT int,
    ERRORMSG nvarchar (MAX)
    ))
    update u SET 
	u.PHOTOID=jd.PHOTOID,
u.PLOTID=jd.PLOTID,
u.PHOTOTYPE=jd.PHOTOTYPE,
u.DESCRIPTION=jd.DESCRIPTION,
u.PHOTONUMBER=jd.PHOTONUMBER,
u.FRAMENUMBER=jd.FRAMENUMBER,
u.AZIMUTH=jd.AZIMUTH,
u.DISTANCE=jd.DISTANCE,
u.Created=jd.Created,
u.LastModified=jd.LastModified,
u.IsDeleted=jd.IsDeleted,
	u.LastModifiedAtServer = CURRENT_TIMESTAMP,
	u.ERRORCOUNT = jd.ERRORCOUNT,
u.ERRORMSG = jd.ERRORMSG
	from PHOTO as u
	inner join json_data as jd on jd.PHOTOID = u.PHOTOID
   WHERE
	dbo.IsPlotCompletefromPlotID(jd.PLOTID) = 0
    RETURN 1
  END TRY
  BEGIN CATCH
     RETURN 0
  END CATCH

END
GO
/****** Object:  StoredProcedure [dbo].[UpdatePlot]    Script Date: 2022-05-30 6:43:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/****** Object:  StoredProcedure [dbo].[InsertProject]    Script Date: 4/13/2020 2:58:46 PM ******/

CREATE PROCEDURE [dbo].[UpdatePlot](@fromjson nvarchar(max))
AS BEGIN

  BEGIN TRY
 
    with json_data as (select PLOTID,PROJECTID,VSNPLOTTYPECODE,VSNPLOTNAME,PLOTKEY,PLOTOVERVIEWDATE,MEASURETYPECODE,LEAD_SPP,GROWTHPLOTNUMBER,CROWN_CLOSURE,FIELD_CREW1,FIELD_CREW2,
FIELD_CREW3,FIELD_CREW4,FIELD_CREW5,FIELD_CREW6,DECLINATION,UTMZONE,EASTING,NORTHING,DATUM,PLOTOVERVIEWNOTE,NONSTANDARDTYPECODE,ACCESSCONDITIONCODE,
MEASUREYEAR,EXISTINGPLOTNAME,EXISTINGPLOTTYPECODE,DISTANCETARGETMOVED,AZIMUTHTARGETMOVED,MATURITYCLASSCODE1,MATURITYCLASSRATIONALE1,MATURITYCLASSCODE2,MATURITYCLASSRATIONALE2,
CANOPYSTRUCTURECODE1,CANOPYSTRUCTURECODE2,MAINCANOPYORIGINCODE1,MAINCANOPYORIGINCODE2,DISTURBANCECODE1,DISTURBANCECODE2,SITERANK,CROWNDAMAGECODE,VIGOURCODE,DAMAGEDESCRIPTION,
FORESTHEALTHDATE,FORESTHEALTHNOTE,FORESTHEALTHPERSON,FOLLOWUPREQUIRED,SMALLTREESHRUBAREA,SMALLTREESHRUBDATE,SMALLTREESHRUBNOTE,SMALLTREEPERSON,
UNDERSTORYVEGETATIONDATE,UNDERSTORYVEGETATIONAREA,UNDERSTORYVEGETATIONNOTE,UNDERSTORYVEGETATIONPERSON,UNDERSTORYCENSUSDATE,UNDERSTORYCENSUSNOTE,UNDERSTORYCENSUSPERSON,DOWNWOODYDEBRISDATE,DOWNWOODYDEBRISNOTE,
DOWNWOODYDEBRISPERSON,DEFORMITYDATE,DEFORMITYNOTE,DEFORMITYPERSON,STANDINFODATE,STANDINFONOTE,STANDINFOPERSON,PERCENTAFFECTED,PERCENTMORTALITY, LINELENGTH1, LINELENGTH2, AGENOTE,AGEPERSON,STEMMAPPINGDATE,
AGEDATE,STEMMAPPINGNOTE,STEMMAPPINGPERSON, Created,LastModified,IsDeleted,ERRORCOUNT,
ERRORMSG
    FROM OPENJSON (@fromjson) 
    WITH (
     PLOTID nvarchar(50),
	PROJECTID nvarchar(50),
	VSNPLOTTYPECODE nvarchar(10),
	VSNPLOTNAME nvarchar(20),
	PLOTKEY nvarchar(max),
	PLOTOVERVIEWDATE nvarchar(50),
	MEASURETYPECODE nvarchar(max),
	LEAD_SPP int,
	GROWTHPLOTNUMBER int,
	CROWN_CLOSURE int,
	FIELD_CREW1 nvarchar(100),
	FIELD_CREW2 nvarchar(100),
	FIELD_CREW3 nvarchar(100),
	FIELD_CREW4 nvarchar(100),
	FIELD_CREW5 nvarchar(100),
	FIELD_CREW6 nvarchar(100),
	DECLINATION int,
	UTMZONE int,
	EASTING float,
	NORTHING float,
	DATUM nvarchar(50),
	PLOTOVERVIEWNOTE nvarchar(max),
	NONSTANDARDTYPECODE int,
	ACCESSCONDITIONCODE int,
	MEASUREYEAR int,
	EXISTINGPLOTNAME nvarchar(50),
	EXISTINGPLOTTYPECODE nvarchar(50),
	DISTANCETARGETMOVED float,
	AZIMUTHTARGETMOVED int,
	MATURITYCLASSCODE1 nvarchar(50),
	MATURITYCLASSRATIONALE1 nvarchar(max),
	MATURITYCLASSCODE2 nvarchar(50),
	MATURITYCLASSRATIONALE2 nvarchar(max),
	CANOPYSTRUCTURECODE1 nvarchar(10),
	CANOPYSTRUCTURECODE2 nvarchar(10),
	MAINCANOPYORIGINCODE1 int,
	MAINCANOPYORIGINCODE2 int,
	DISTURBANCECODE1 int,
	DISTURBANCECODE2 int,
	SITERANK int,
	CROWNDAMAGECODE int,
	VIGOURCODE int,
	DAMAGEDESCRIPTION nvarchar(max),
	FORESTHEALTHDATE nvarchar(50),
	FORESTHEALTHNOTE nvarchar(max),
	FORESTHEALTHPERSON nvarchar(100),
	FOLLOWUPREQUIRED nvarchar(1),
	SMALLTREESHRUBAREA int,
	SMALLTREESHRUBDATE nvarchar(50),
	SMALLTREESHRUBNOTE nvarchar(max),
	SMALLTREEPERSON nvarchar(100),
	UNDERSTORYVEGETATIONDATE nvarchar(50),
	UNDERSTORYVEGETATIONAREA int,
	UNDERSTORYVEGETATIONNOTE nvarchar(max),
	UNDERSTORYVEGETATIONPERSON nvarchar(100),
	UNDERSTORYCENSUSDATE nvarchar(50),
	UNDERSTORYCENSUSNOTE nvarchar(max),
	UNDERSTORYCENSUSPERSON nvarchar(100),
	DOWNWOODYDEBRISDATE nvarchar(50),
	DOWNWOODYDEBRISNOTE nvarchar(max),
	DOWNWOODYDEBRISPERSON nvarchar(100),
	DEFORMITYDATE nvarchar(50),
	DEFORMITYNOTE nvarchar(max),
	DEFORMITYPERSON nvarchar(100),
	STANDINFODATE nvarchar(50),
	STANDINFONOTE nvarchar(max),
	STANDINFOPERSON nvarchar(100),
	PERCENTAFFECTED int,
	PERCENTMORTALITY int,
    LINELENGTH1 float,
	LINELENGTH2 float,
	AGENOTE  nvarchar(max),
	AGEPERSON  nvarchar(100),
	STEMMAPPINGDATE nvarchar(50),
    AGEDATE nvarchar(50),
	STEMMAPPINGNOTE nvarchar(max),
	STEMMAPPINGPERSON  nvarchar(100),
	Created datetime2,
	LastModified datetime2,
	IsDeleted nvarchar(1),
	ERRORCOUNT int,
    ERRORMSG nvarchar(MAX)
    ))
    update u SET 
u.PLOTID=jd.PLOTID,
u.PROJECTID=jd.PROJECTID,
u.VSNPLOTTYPECODE=jd.VSNPLOTTYPECODE,
u.VSNPLOTNAME=jd.VSNPLOTNAME,
u.PLOTKEY=jd.PLOTKEY,
u.PLOTOVERVIEWDATE=jd.PLOTOVERVIEWDATE,
u.MEASURETYPECODE=jd.MEASURETYPECODE,
u.LEAD_SPP=jd.LEAD_SPP,
u.GROWTHPLOTNUMBER=jd.GROWTHPLOTNUMBER,
u.CROWN_CLOSURE=jd.CROWN_CLOSURE,
u.FIELD_CREW1=jd.FIELD_CREW1,
u.FIELD_CREW2=jd.FIELD_CREW2,
u.FIELD_CREW3=jd.FIELD_CREW3,
u.FIELD_CREW4=jd.FIELD_CREW4,
u.FIELD_CREW5=jd.FIELD_CREW5,
u.FIELD_CREW6=jd.FIELD_CREW6,
u.DECLINATION=jd.DECLINATION,
u.UTMZONE=jd.UTMZONE,
u.EASTING=jd.EASTING,
u.NORTHING=jd.NORTHING,
u.DATUM=jd.DATUM,
u.PLOTOVERVIEWNOTE=jd.PLOTOVERVIEWNOTE,
u.NONSTANDARDTYPECODE=jd.NONSTANDARDTYPECODE,
u.ACCESSCONDITIONCODE=jd.ACCESSCONDITIONCODE,
u.MEASUREYEAR=jd.MEASUREYEAR,
u.EXISTINGPLOTNAME=jd.EXISTINGPLOTNAME,
u.EXISTINGPLOTTYPECODE=jd.EXISTINGPLOTTYPECODE,
u.DISTANCETARGETMOVED=jd.DISTANCETARGETMOVED,
u.AZIMUTHTARGETMOVED=jd.AZIMUTHTARGETMOVED,
u.MATURITYCLASSCODE1=jd.MATURITYCLASSCODE1,
u.MATURITYCLASSRATIONALE1=jd.MATURITYCLASSRATIONALE1,
u.MATURITYCLASSCODE2=jd.MATURITYCLASSCODE2,
u.MATURITYCLASSRATIONALE2=jd.MATURITYCLASSRATIONALE2,
u.CANOPYSTRUCTURECODE1=jd.CANOPYSTRUCTURECODE1,
u.CANOPYSTRUCTURECODE2=jd.CANOPYSTRUCTURECODE2,
u.MAINCANOPYORIGINCODE1=jd.MAINCANOPYORIGINCODE1,
u.MAINCANOPYORIGINCODE2=jd.MAINCANOPYORIGINCODE2,
u.DISTURBANCECODE1=jd.DISTURBANCECODE1,
u.DISTURBANCECODE2=jd.DISTURBANCECODE2,
u.SITERANK=jd.SITERANK,
u.CROWNDAMAGECODE=jd.CROWNDAMAGECODE,
u.VIGOURCODE=jd.VIGOURCODE,
u.DAMAGEDESCRIPTION=jd.DAMAGEDESCRIPTION,
u.FORESTHEALTHDATE=jd.FORESTHEALTHDATE,
u.FORESTHEALTHNOTE=jd.FORESTHEALTHNOTE,
u.FORESTHEALTHPERSON=jd.FORESTHEALTHPERSON,
u.FOLLOWUPREQUIRED=jd.FOLLOWUPREQUIRED,
u.SMALLTREESHRUBAREA=jd.SMALLTREESHRUBAREA,
u.SMALLTREESHRUBDATE=jd.SMALLTREESHRUBDATE,
u.SMALLTREESHRUBNOTE=jd.SMALLTREESHRUBNOTE,
u.SMALLTREEPERSON=jd.SMALLTREEPERSON,
u.UNDERSTORYVEGETATIONDATE=jd.UNDERSTORYVEGETATIONDATE,
u.UNDERSTORYVEGETATIONAREA=jd.UNDERSTORYVEGETATIONAREA,
u.UNDERSTORYVEGETATIONNOTE=jd.UNDERSTORYVEGETATIONNOTE,
u.UNDERSTORYVEGETATIONPERSON=jd.UNDERSTORYVEGETATIONPERSON,
u.UNDERSTORYCENSUSDATE=jd.UNDERSTORYCENSUSDATE,
u.UNDERSTORYCENSUSNOTE=jd.UNDERSTORYCENSUSNOTE,
u.UNDERSTORYCENSUSPERSON=jd.UNDERSTORYCENSUSPERSON,
u.DOWNWOODYDEBRISDATE=jd.DOWNWOODYDEBRISDATE,
u.DOWNWOODYDEBRISNOTE=jd.DOWNWOODYDEBRISNOTE,
u.DOWNWOODYDEBRISPERSON=jd.DOWNWOODYDEBRISPERSON,
u.DEFORMITYDATE=jd.DEFORMITYDATE,
u.DEFORMITYNOTE=jd.DEFORMITYNOTE,
u.DEFORMITYPERSON=jd.DEFORMITYPERSON,
u.STANDINFODATE=jd.STANDINFODATE,
u.STANDINFONOTE=jd.STANDINFONOTE,
u.STANDINFOPERSON=jd.STANDINFOPERSON,
u.PERCENTAFFECTED=jd.PERCENTAFFECTED,
u.PERCENTMORTALITY=jd.PERCENTMORTALITY,
u.LINELENGTH1=jd.LINELENGTH1,
u.LINELENGTH2=jd.LINELENGTH2,
u.AGENOTE = jd.AGENOTE,
u.AGEPERSON = jd.AGEPERSON,
u.STEMMAPPINGDATE = jd.STEMMAPPINGDATE,
u.AGEDATE=jd.AGEDATE,
u.STEMMAPPINGNOTE = jd.STEMMAPPINGNOTE,
u.STEMMAPPINGPERSON = jd.STEMMAPPINGPERSON,
u.Created=jd.Created,
u.LastModified=jd.LastModified,
u.IsDeleted=jd.IsDeleted,
	u.LastModifiedAtServer = CURRENT_TIMESTAMP,
	u.ERRORCOUNT = jd.ERRORCOUNT,
u.ERRORMSG = jd.ERRORMSG
	from PLOT as u
	inner join json_data as jd on jd.PLOTID = u.PLOTID 
	WHERE
	dbo.IsPlotCompletefromPlotID(jd.PLOTID) = 0
   
    RETURN 1
  END TRY
  BEGIN CATCH
     RETURN 0
  END CATCH

END
GO
/****** Object:  StoredProcedure [dbo].[UpdateProject]    Script Date: 2022-05-30 6:43:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****** Object:  StoredProcedure [dbo].[InsertProject]    Script Date: 4/13/2020 2:58:46 PM ******/

CREATE PROCEDURE [dbo].[UpdateProject](@fromjson nvarchar(max))
AS BEGIN

  BEGIN TRY
 
    with json_data as (select PROJECTID, NAME, DESCRIPTION, PROJECT_DATE, Created, LastModified as LastModified 
    FROM OPENJSON (@fromjson) 
    WITH (
            PROJECTID nvarchar(max),
            NAME nvarchar(max),
            DESCRIPTION nvarchar(max),
            PROJECT_DATE nvarchar(max),
			Created datetime2,
			LastModified datetime2
    ))
    update u SET 
	NAME = u.NAME,
	u.DESCRIPTION = jd.DESCRIPTION,
	u.PROJECT_DATE = jd.PROJECT_DATE,
	u.LastModified = jd.LastModified,
	u.LastModifiedAtServer = CURRENT_TIMESTAMP
	from PROJECT as u
	inner join json_data as jd on jd.PROJECTID = u.PROJECTID
   
    RETURN 1
  END TRY
  BEGIN CATCH
     RETURN 0
  END CATCH

END
GO
/****** Object:  StoredProcedure [dbo].[UpdateSmalltree]    Script Date: 2022-05-30 6:43:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/****** Object:  StoredProcedure [dbo].[InsertProject]    Script Date: 4/13/2020 2:58:46 PM ******/

CREATE PROCEDURE [dbo].[UpdateSmalltree](@fromjson nvarchar(max))
AS BEGIN

  BEGIN TRY
 
    with json_data as (select SMALLTREEID,PLOTID,SPECIESCODE,HT_CLASS1_COUNT,HT_CLASS2_COUNT,HT_CLASS3_COUNT,HT_CLASS4_COUNT,HT_CLASS5_COUNT,HT_CLASS6_COUNT,HT_CLASS7_COUNT,HT_CLASS8_COUNT,HEIGHT, [COUNT],
Created,LastModified,IsDeleted,ERRORCOUNT,
ERRORMSG
    FROM OPENJSON (@fromjson) 
    WITH (
SMALLTREEID nvarchar(50),
PLOTID nvarchar(50),
SPECIESCODE int,
HT_CLASS1_COUNT int,
HT_CLASS2_COUNT int,
HT_CLASS3_COUNT int,
HT_CLASS4_COUNT int,
HT_CLASS5_COUNT int,
HT_CLASS6_COUNT int,
HT_CLASS7_COUNT int,
HT_CLASS8_COUNT int,
Created datetime2,
LastModified datetime2,
IsDeleted nvarchar(1),
HEIGHT float,
[COUNT] int,
ERRORCOUNT int,
ERRORMSG nvarchar(MAX)
    ))
    update u SET 
u.SMALLTREEID=jd.SMALLTREEID,
u.PLOTID=jd.PLOTID,
u.SPECIESCODE=jd.SPECIESCODE,
u.HT_CLASS1_COUNT=jd.HT_CLASS1_COUNT,
u.HT_CLASS2_COUNT=jd.HT_CLASS2_COUNT,
u.HT_CLASS3_COUNT=jd.HT_CLASS3_COUNT,
u.HT_CLASS4_COUNT=jd.HT_CLASS4_COUNT,
u.HT_CLASS5_COUNT=jd.HT_CLASS5_COUNT,
u.HT_CLASS6_COUNT=jd.HT_CLASS6_COUNT,
u.HT_CLASS7_COUNT=jd.HT_CLASS7_COUNT,
u.HT_CLASS8_COUNT=jd.HT_CLASS8_COUNT,
u.HEIGHT  = jd.HEIGHT,
u.[COUNT] = jd.[COUNT],
u.Created=jd.Created,
u.LastModified=jd.LastModified,
u.IsDeleted=jd.IsDeleted,
	u.LastModifiedAtServer = CURRENT_TIMESTAMP,
	u.ERRORCOUNT = jd.ERRORCOUNT,
u.ERRORMSG = jd.ERRORMSG
	from SMALLTREE as u
	inner join json_data as jd on jd.SMALLTREEID = u.SMALLTREEID
   WHERE
	dbo.IsPlotCompletefromPlotID(jd.PLOTID) = 0
    RETURN 1
  END TRY
  BEGIN CATCH
     RETURN 0
  END CATCH

END
GO
/****** Object:  StoredProcedure [dbo].[UpdateSmalltreeTally]    Script Date: 2022-05-30 6:43:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


/****** Object:  StoredProcedure [dbo].[InsertProject]    Script Date: 4/13/2020 2:58:46 PM ******/

CREATE PROCEDURE [dbo].[UpdateSmalltreeTally](@fromjson nvarchar(max))
AS BEGIN

  BEGIN TRY
 
    with json_data as (select SMALLTREETALLYID,PLOTID,SPECIESCODE,DBH, HEIGHT, [COUNT], Created,LastModified,IsDeleted, ERRORCOUNT, ERRORMSG
    FROM OPENJSON (@fromjson) 
    WITH (
SMALLTREETALLYID nvarchar(50),
PLOTID nvarchar(50),
SPECIESCODE int,
DBH float,
HEIGHT float,
[COUNT] int,
Created datetime2,
LastModified datetime2,
IsDeleted nvarchar(1),
ERRORCOUNT int,
ERRORMSG nvarchar(MAX)
    ))
    update u SET 
u.SMALLTREETALLYID=jd.SMALLTREETALLYID,
u.PLOTID=jd.PLOTID,
u.SPECIESCODE=jd.SPECIESCODE,
u.DBH = jd.DBH,
u.HEIGHT  = jd.HEIGHT,
u.[COUNT] = jd.[COUNT],
u.Created=jd.Created,
u.LastModified=jd.LastModified,
u.IsDeleted=jd.IsDeleted,
u.ERRORCOUNT = jd.ERRORCOUNT,
u.ERRORMSG = jd.ERRORMSG,
	u.LastModifiedAtServer = CURRENT_TIMESTAMP
	from SMALLTREETALLY as u
	inner join json_data as jd on jd.SMALLTREETALLYID = u.SMALLTREETALLYID
   WHERE
	dbo.IsPlotCompletefromPlotID(jd.PLOTID) = 0
    RETURN 1
  END TRY
  BEGIN CATCH
     RETURN 0
  END CATCH

END
GO
/****** Object:  StoredProcedure [dbo].[UpdateSoil]    Script Date: 2022-05-30 6:43:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/****** Object:  StoredProcedure [dbo].[InsertProject]    Script Date: 4/13/2020 2:58:46 PM ******/

CREATE PROCEDURE [dbo].[UpdateSoil](@fromjson nvarchar(max))
AS BEGIN

  BEGIN TRY
 
    with json_data as (select SOILID,PLOTID,HORIZONNUMBER,DEPTHFROM,DEPTHTO,HORIZON,DECOMPOSITIONCODE,MINERALTEXTURECODE,POREPATTERNCODE,STRUCTURE,MATRIXCOLOUR,
MOTTLECOLOUR,PERCENTGRAVEL,PERCENTCOBBLE,PERCENTSTONE,GLEYCOLOUR,Created,LastModified,IsDeleted, ERRORCOUNT, ERRORMSG
    FROM OPENJSON (@fromjson) 
    WITH (
SOILID nvarchar(50),
PLOTID nvarchar(50),
HORIZONNUMBER int,
[DEPTHFROM] float,
[DEPTHTO] float,
HORIZON nvarchar(50),
DECOMPOSITIONCODE nvarchar(50),
MINERALTEXTURECODE nvarchar(50),
POREPATTERNCODE nvarchar(50),
STRUCTURE nvarchar(50),
MATRIXCOLOUR nvarchar(50),
MOTTLECOLOUR nvarchar(50),
PERCENTGRAVEL int,
PERCENTCOBBLE int,
PERCENTSTONE int,
GLEYCOLOUR nvarchar(50),
Created datetime2,
LastModified datetime2,
IsDeleted nvarchar(1),
ERRORCOUNT int,
ERRORMSG nvarchar(max)
    ))
    update u SET 
u.SOILID=jd.SOILID,
u.PLOTID=jd.PLOTID,
u.HORIZONNUMBER=jd.HORIZONNUMBER,
u.[DEPTHFROM]=jd.[DEPTHFROM],
u.[DEPTHTO]=jd.[DEPTHTO],
u.HORIZON=jd.HORIZON,
u.DECOMPOSITIONCODE=jd.DECOMPOSITIONCODE,
u.MINERALTEXTURECODE=jd.MINERALTEXTURECODE,
u.POREPATTERNCODE=jd.POREPATTERNCODE,
u.STRUCTURE=jd.STRUCTURE,
u.MATRIXCOLOUR=jd.MATRIXCOLOUR,
u.MOTTLECOLOUR=jd.MOTTLECOLOUR,
u.PERCENTGRAVEL=jd.PERCENTGRAVEL,
u.PERCENTCOBBLE=jd.PERCENTCOBBLE,
u.PERCENTSTONE=jd.PERCENTSTONE,
u.GLEYCOLOUR=jd.GLEYCOLOUR,
u.Created=jd.Created,
u.LastModified=jd.LastModified,
u.IsDeleted=jd.IsDeleted,
u.LastModifiedAtServer = CURRENT_TIMESTAMP,
u.ERRORCOUNT = jd.ERRORCOUNT,
u.ERRORMSG = jd.ERRORMSG

	from SOIL as u
	inner join json_data as jd on jd.SOILID = u.SOILID
  WHERE
	dbo.IsPlotCompletefromPlotID(jd.PLOTID) = 0
    RETURN 1
  END TRY
  BEGIN CATCH
     RETURN 0
  END CATCH

END
GO
/****** Object:  StoredProcedure [dbo].[UpdateStemmap]    Script Date: 2022-05-30 6:43:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UpdateStemmap](@fromjson nvarchar(max))
AS BEGIN

  BEGIN TRY
 
    with json_data as (select STEMMAPID,TREEID,AZIMUTH,DISTANCE,CROWNWIDTH1,CROWNWIDTH2,CROWNOFFSETAZIMUTH,CROWNOFFSETDISTANCE,Created,LastModified,IsDeleted,ERRORCOUNT,
ERRORMSG
    FROM OPENJSON (@fromjson) 
    WITH (
STEMMAPID nvarchar(50),
TREEID nvarchar(50),
AZIMUTH int,
DISTANCE float,
CROWNWIDTH1 float,
CROWNWIDTH2 float,
CROWNOFFSETAZIMUTH int,
CROWNOFFSETDISTANCE float,
Created datetime2,
LastModified datetime2,
IsDeleted nvarchar(1),
ERRORCOUNT int,
ERRORMSG nvarchar(MAX)
    ))
    update u SET 
u.STEMMAPID=jd.STEMMAPID,
u.TREEID=jd.TREEID,
u.AZIMUTH=jd.AZIMUTH,
u.DISTANCE=jd.DISTANCE,
u.CROWNWIDTH1=jd.CROWNWIDTH1,
u.CROWNWIDTH2=jd.CROWNWIDTH2,
u.CROWNOFFSETAZIMUTH=jd.CROWNOFFSETAZIMUTH,
u.CROWNOFFSETDISTANCE=jd.CROWNOFFSETDISTANCE,
u.Created=jd.Created,
u.LastModified=jd.LastModified,
u.IsDeleted=jd.IsDeleted,
u.LastModifiedAtServer = CURRENT_TIMESTAMP,
u.ERRORCOUNT = jd.ERRORCOUNT,
u.ERRORMSG = jd.ERRORMSG
	from STEMMAP as u
	inner join json_data as jd on jd.STEMMAPID = u.STEMMAPID
	  WHERE
	dbo.IsPlotCompletefromTreeID(jd.TREEID) = 0
  
    RETURN 1
  END TRY
  BEGIN CATCH
     RETURN 0
  END CATCH

END
GO
/****** Object:  StoredProcedure [dbo].[UpdateTree]    Script Date: 2022-05-30 6:43:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UpdateTree](@fromjson nvarchar(max))
AS BEGIN

  BEGIN TRY
 
    with json_data as (select TREEID,PLOTID,SECTION,TREENUMBER,SPECIESCODE,TAG_TYPE,TREEORIGINCODE,TREESTATUSCODE,VIGOURCODE,HEIGHTTODBH,DBH,HT,
LENGTH,DBHIN,CROWNIN,LIVE_CROWN_RATIO,CROWNCLASSCODE,CROWN_POSITION,CROWNDAMAGECODE,DEFOLIATING_INSECT,FOLIAR_DISEASE,STEMQUALITYCODE,BARKRETENTIONCODE,
WOODCONDITIONCODE,DECAYCLASS,MORTALITYCAUSECODE,BROKENTOP,AZIMUTH,DISTANCE,CROWNWIDTH1,CROWNWIDTH2,COMMENTS,DBH1,
DBH2,DIRECTTOTALHEIGHT,OCULARTOTALHEIGHT,HEIGHTTODEADTIP,DIRECTHEIGHTTOCONTLIVECROWN,OCULARHEIGHTTOCONTLIVECROWN,
DIRECTOFFSETDISTANCE,DEGREEOFLEAN,HEIGHTTOCORE,CORESTATUSCODE,SOUNDWOODLENGTH,FIELDAGE,OFFICERINGCOUNT,MISSINGRINGS,VSNSTATUSCODE,
Created,LastModified,IsDeleted,ERRORCOUNT,ERRORMSG
    FROM OPENJSON (@fromjson) 
    WITH (
TREEID nvarchar(50),
PLOTID nvarchar(50),
SECTION int,
TREENUMBER int,
SPECIESCODE int,
TAG_TYPE nvarchar(50),
TREEORIGINCODE nvarchar(10),
TREESTATUSCODE nvarchar(10),
VIGOURCODE int,
HEIGHTTODBH float,
DBH float,
HT float,
LENGTH float,
DBHIN nvarchar(1),
CROWNIN nvarchar(1),
LIVE_CROWN_RATIO int,
CROWNCLASSCODE nvarchar(10),
CROWN_POSITION int,
CROWNDAMAGECODE int,
DEFOLIATING_INSECT int,
FOLIAR_DISEASE int,
STEMQUALITYCODE nvarchar(10),
BARKRETENTIONCODE int,
WOODCONDITIONCODE int,
DECAYCLASS int,
MORTALITYCAUSECODE int,
BROKENTOP nvarchar(1),
AZIMUTH float,
DISTANCE float,
CROWNWIDTH1 float,
CROWNWIDTH2 float,
COMMENTS nvarchar(max),
DBH1 float,
DBH2	float,
DIRECTTOTALHEIGHT	float,
OCULARTOTALHEIGHT	float,
HEIGHTTODEADTIP	float,
DIRECTHEIGHTTOCONTLIVECROWN	float,
OCULARHEIGHTTOCONTLIVECROWN	float,
DIRECTOFFSETDISTANCE	float,
DEGREEOFLEAN	int,
HEIGHTTOCORE	float,
CORESTATUSCODE	nvarchar(10),
SOUNDWOODLENGTH	float,
FIELDAGE	int,
OFFICERINGCOUNT	int,
MISSINGRINGS	int,
VSNSTATUSCODE nvarchar(10),
Created datetime2,
LastModified datetime2,
IsDeleted nvarchar(1),
ERRORCOUNT int,
ERRORMSG nvarchar(MAX)
    ))
    update u SET 
u.TREEID=jd.TREEID,
u.PLOTID=jd.PLOTID,
u.SECTION=jd.SECTION,
u.TREENUMBER=jd.TREENUMBER,
u.SPECIESCODE=jd.SPECIESCODE,
u.TAG_TYPE=jd.TAG_TYPE,
u.TREEORIGINCODE=jd.TREEORIGINCODE,
u.TREESTATUSCODE=jd.TREESTATUSCODE,
u.VIGOURCODE=jd.VIGOURCODE,
u.HEIGHTTODBH=jd.HEIGHTTODBH,
u.DBH=jd.DBH,
u.HT=jd.HT,
u.LENGTH=jd.LENGTH,
u.DBHIN=jd.DBHIN,
u.CROWNIN=jd.CROWNIN,
u.LIVE_CROWN_RATIO=jd.LIVE_CROWN_RATIO,
u.CROWNCLASSCODE=jd.CROWNCLASSCODE,
u.CROWN_POSITION=jd.CROWN_POSITION,
u.CROWNDAMAGECODE=jd.CROWNDAMAGECODE,
u.DEFOLIATING_INSECT=jd.DEFOLIATING_INSECT,
u.FOLIAR_DISEASE=jd.FOLIAR_DISEASE,
u.STEMQUALITYCODE=jd.STEMQUALITYCODE,
u.BARKRETENTIONCODE=jd.BARKRETENTIONCODE,
u.WOODCONDITIONCODE=jd.WOODCONDITIONCODE,
u.DECAYCLASS=jd.DECAYCLASS,
u.MORTALITYCAUSECODE=jd.MORTALITYCAUSECODE,
u.BROKENTOP=jd.BROKENTOP,
u.AZIMUTH=jd.AZIMUTH,
u.DISTANCE=jd.DISTANCE,
u.CROWNWIDTH1=jd.CROWNWIDTH1,
u.CROWNWIDTH2=jd.CROWNWIDTH2,
u.COMMENTS=jd.COMMENTS,
u.DBH1=jd.DBH1,
u.DBH2=jd.DBH2,
u.DIRECTTOTALHEIGHT=jd.DIRECTTOTALHEIGHT,
u.OCULARTOTALHEIGHT=jd.OCULARTOTALHEIGHT,
u.HEIGHTTODEADTIP=jd.HEIGHTTODEADTIP,
u.DIRECTHEIGHTTOCONTLIVECROWN=jd.DIRECTHEIGHTTOCONTLIVECROWN,
u.OCULARHEIGHTTOCONTLIVECROWN=jd.OCULARHEIGHTTOCONTLIVECROWN,
u.DIRECTOFFSETDISTANCE=jd.DIRECTOFFSETDISTANCE,
u.DEGREEOFLEAN=jd.DEGREEOFLEAN,
u.HEIGHTTOCORE=jd.HEIGHTTOCORE,
u.CORESTATUSCODE=jd.CORESTATUSCODE,
u.SOUNDWOODLENGTH=jd.SOUNDWOODLENGTH,
u.FIELDAGE=jd.FIELDAGE,
u.OFFICERINGCOUNT=jd.OFFICERINGCOUNT,
u.MISSINGRINGS=jd.MISSINGRINGS,
u.VSNSTATUSCODE=jd.VSNSTATUSCODE,
u.Created=jd.Created,
u.LastModified=jd.LastModified,
u.IsDeleted=jd.IsDeleted,
u.LastModifiedAtServer = CURRENT_TIMESTAMP,
u.ERRORCOUNT = jd.ERRORCOUNT,
u.ERRORMSG = jd.ERRORMSG
	from TREE as u
	inner join json_data as jd on jd.TREEID = u.TREEID
  WHERE
	dbo.IsPlotCompletefromPlotID(jd.PLOTID) = 0
    RETURN 1
  END TRY
  BEGIN CATCH
     RETURN 0
  END CATCH

END
GO
/****** Object:  StoredProcedure [dbo].[UpdateVegetation]    Script Date: 2022-05-30 6:43:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/****** Object:  StoredProcedure [dbo].[InsertProject]    Script Date: 4/13/2020 2:58:46 PM ******/

CREATE PROCEDURE [dbo].[UpdateVegetation](@fromjson nvarchar(max))
AS BEGIN

  BEGIN TRY
 
    with json_data as (select VEGETATIONID,PLOTID,VSNSPECIESCODE,SPECIMENNUMBER,QUAD1,QUAD2,QUAD3,QUAD4,ELCLAYER3,ELCLAYER4,ELCLAYER5,ELCLAYER6,ELCLAYER7,Created,LastModified,IsDeleted,
	QUAD1_ELC4,QUAD2_ELC4,QUAD3_ELC4,QUAD4_ELC4,
	QUAD1_ELC5,QUAD2_ELC5,QUAD3_ELC5,QUAD4_ELC5,
	QUAD1_ELC6,QUAD2_ELC6,QUAD3_ELC6,QUAD4_ELC6,
	QUAD1_ELC7,QUAD2_ELC7,QUAD3_ELC7,QUAD4_ELC7,ERRORCOUNT,ERRORMSG

    FROM OPENJSON (@fromjson) 
    WITH (
VEGETATIONID nvarchar(50),
PLOTID nvarchar(50),
VSNSPECIESCODE nvarchar(50),
SPECIMENNUMBER int,
QUAD1 float,
QUAD2 float,
QUAD3 float,
QUAD4 float,
ELCLAYER3 float,
ELCLAYER4 float,
ELCLAYER5 float,
ELCLAYER6 float,
ELCLAYER7 float,
Created datetime2,
LastModified datetime2,
IsDeleted nvarchar(1),
QUAD1_ELC4 float,
QUAD2_ELC4 float,
QUAD3_ELC4 float,
QUAD4_ELC4 float,
QUAD1_ELC5 float,
QUAD2_ELC5 float,
QUAD3_ELC5 float,
QUAD4_ELC5 float,
QUAD1_ELC6 float,
QUAD2_ELC6 float,
QUAD3_ELC6 float,
QUAD4_ELC6 float,
QUAD1_ELC7 float,
QUAD2_ELC7 float,
QUAD3_ELC7 float,
QUAD4_ELC7 float,
ERRORCOUNT int,
ERRORMSG nvarchar(MAX)
    ))
    update u SET 
u.VEGETATIONID=jd.VEGETATIONID,
u.PLOTID=jd.PLOTID,
u.VSNSPECIESCODE=jd.VSNSPECIESCODE,
u.SPECIMENNUMBER=jd.SPECIMENNUMBER,
u.QUAD1=jd.QUAD1,
u.QUAD2=jd.QUAD2,
u.QUAD3=jd.QUAD3,
u.QUAD4=jd.QUAD4,
u.ELCLAYER3=jd.ELCLAYER3,
u.ELCLAYER4=jd.ELCLAYER4,
u.ELCLAYER5=jd.ELCLAYER5,
u.ELCLAYER6=jd.ELCLAYER6,
u.ELCLAYER7=jd.ELCLAYER7,
u.Created=jd.Created,
u.LastModified=jd.LastModified,
u.IsDeleted=jd.IsDeleted,
u.LastModifiedAtServer = CURRENT_TIMESTAMP,
u.QUAD1_ELC4 = jd.QUAD1_ELC4,
u.QUAD2_ELC4 = jd.QUAD2_ELC4,
u.QUAD3_ELC4 = jd.QUAD3_ELC4,
u.QUAD4_ELC4 = jd.QUAD4_ELC4,
u.QUAD1_ELC5 = jd.QUAD1_ELC5,
u.QUAD2_ELC5 = jd.QUAD2_ELC5,
u.QUAD3_ELC5 = jd.QUAD3_ELC5,
u.QUAD4_ELC5 = jd.QUAD4_ELC5,
u.QUAD1_ELC6 = jd.QUAD1_ELC6,
u.QUAD2_ELC6 = jd.QUAD2_ELC6,
u.QUAD3_ELC6 = jd.QUAD3_ELC6,
u.QUAD4_ELC6 = jd.QUAD4_ELC6,
u.QUAD1_ELC7 = jd.QUAD1_ELC7,
u.QUAD2_ELC7 = jd.QUAD2_ELC7,
u.QUAD3_ELC7 = jd.QUAD3_ELC7,
u.QUAD4_ELC7 = jd.QUAD4_ELC7,
u.ERRORCOUNT = jd.ERRORCOUNT,
u.ERRORMSG = jd.ERRORMSG
	from VEGETATION as u
	inner join json_data as jd on jd.VEGETATIONID = u.VEGETATIONID
  WHERE
	dbo.IsPlotCompletefromPlotID(jd.PLOTID) = 0
    RETURN 1
  END TRY
  BEGIN CATCH
     RETURN 0
  END CATCH

END
GO
/****** Object:  StoredProcedure [dbo].[UpdateVegetationCensus]    Script Date: 2022-05-30 6:43:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/****** Object:  StoredProcedure [dbo].[InsertProject]    Script Date: 4/13/2020 2:58:46 PM ******/

CREATE PROCEDURE [dbo].[UpdateVegetationCensus](@fromjson nvarchar(max))
AS BEGIN

  BEGIN TRY
 
    with json_data as (select VEGETATIONCENSUSID,PLOTID,VSNSPECIESCODE,SPECIMENNUMBER,Created,LastModified,IsDeleted,ERRORCOUNT,ERRORMSG
    FROM OPENJSON (@fromjson) 
    WITH (
VEGETATIONCENSUSID nvarchar(50),
PLOTID nvarchar(50),
VSNSPECIESCODE nvarchar(50),
SPECIMENNUMBER int,
Created datetime2,
LastModified datetime2,
IsDeleted nvarchar(1),
ERRORCOUNT int,
ERRORMSG nvarchar(MAX)
    ))
    update u SET 
u.VEGETATIONCENSUSID=jd.VEGETATIONCENSUSID,
u.PLOTID=jd.PLOTID,
u.VSNSPECIESCODE=jd.VSNSPECIESCODE,
u.SPECIMENNUMBER=jd.SPECIMENNUMBER,
u.Created=jd.Created,
u.LastModified=jd.LastModified,
u.IsDeleted=jd.IsDeleted,
u.LastModifiedAtServer = CURRENT_TIMESTAMP,
u.ERRORCOUNT = jd.ERRORCOUNT,
u.ERRORMSG = jd.ERRORMSG
	from VEGETATIONCENSUS as u
	inner join json_data as jd on jd.VEGETATIONCENSUSID = u.VEGETATIONCENSUSID
  WHERE
	dbo.IsPlotCompletefromPlotID(jd.PLOTID) = 0
    RETURN 1
  END TRY
  BEGIN CATCH
     RETURN 0
  END CATCH

END
GO
