/****** Object:  Database [eLiDAR]    Script Date: 4/14/2020 9:39:58 PM ******/
CREATE DATABASE [eLiDAR]  (EDITION = 'Basic', SERVICE_OBJECTIVE = 'Basic', MAXSIZE = 2 GB) WITH CATALOG_COLLATION = SQL_Latin1_General_CP1_CI_AS;
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
ALTER DATABASE [eLiDAR] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 7), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 10, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO)
GO
/****** Object:  User [datacollector]    Script Date: 4/14/2020 9:39:58 PM ******/
CREATE USER [datacollector] FOR LOGIN [datacollector] WITH DEFAULT_SCHEMA=[dbo]
GO
sys.sp_addrolemember @rolename = N'db_datareader', @membername = N'datacollector'
GO
sys.sp_addrolemember @rolename = N'db_datawriter', @membername = N'datacollector'
GO
/****** Object:  Table [dbo].[DEFORMITY]    Script Date: 4/14/2020 9:40:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DEFORMITY](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[DEFORMITYID] [nvarchar](50) NULL,
	[TREEID] [text] NULL,
	[TYPE] [int] NULL,
	[CAUSE] [int] NULL,
	[HT_FROM] [float] NULL,
	[HT_TO] [float] NULL,
	[QUAD] [text] NULL,
	[EXTENT] [int] NULL,
	[LEAN] [int] NULL,
	[AZIMUTH] [int] NULL,
	[LENGTH] [float] NULL,
	[WIDTH] [float] NULL,
	[PCT_SCUFF] [int] NULL,
	[PCT_SCRAPE] [int] NULL,
	[PCT_GOUGE] [int] NULL,
	[OLD_FEEDING_CAVITY] [text] NULL,
	[NEW_FEEDING_CAVITY] [text] NULL,
	[OLD_NEST_CAVITY] [text] NULL,
	[NEW_NEST_CAVITY] [text] NULL,
	[STICK_NEST] [text] NULL,
	[Created] [datetime2](7) NULL,
	[LastModified] [datetime2](7) NULL,
	[IsDeleted] [text] NULL,
	[CreatedAtServer] [datetime2](7) NULL,
	[LastModifiedAtServer] [datetime2](7) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UC_DEFORMITYID] UNIQUE NONCLUSTERED 
(
	[DEFORMITYID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DWD]    Script Date: 4/14/2020 9:40:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DWD](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[DWDID] [nvarchar](50) NULL,
	[PLOTID] [text] NULL,
	[LINE] [int] NULL,
	[DWDNUM] [int] NULL,
	[SPECIES] [int] NULL,
	[DIAM] [float] NULL,
	[DECOMP_CLASS] [int] NULL,
	[ORIGIN] [text] NULL,
	[TILT_ANGLE] [int] NULL,
	[LENGTH] [float] NULL,
	[SMALL_DIAM] [float] NULL,
	[LARGE_DIAM] [float] NULL,
	[GT_50_MOSS] [text] NULL,
	[BURNED] [text] NULL,
	[HOLLOW] [text] NULL,
	[IS_ACCUM] [text] NULL,
	[ACCUM_LENGTH] [float] NULL,
	[ACCUM_DEPTH] [float] NULL,
	[PERCENT_CONIFER] [int] NULL,
	[PERCENT_DECID] [int] NULL,
	[Created] [datetime2](7) NULL,
	[LastModified] [datetime2](7) NULL,
	[IsDeleted] [text] NULL,
	[CreatedAtServer] [datetime2](7) NULL,
	[LastModifiedAtServer] [datetime2](7) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UC_DWDID] UNIQUE NONCLUSTERED 
(
	[DWDID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ECOSITE]    Script Date: 4/14/2020 9:40:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ECOSITE](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ECOSITEID] [nvarchar](50) NULL,
	[PLOTID] [text] NULL,
	[HUMUS_FORM] [int] NULL,
	[DRAINAGE] [int] NULL,
	[STRATIFIED] [text] NULL,
	[EFFECTIVE_PORE_PATTERN] [text] NULL,
	[ELC_SUBSTRATE_TYPE] [text] NULL,
	[DEPTH_TO_DISTINCT_MOTTLES] [int] NULL,
	[DEPTH_TO_PROMINENT_MOTTLES] [int] NULL,
	[DEPTH_TO_GLEY] [int] NULL,
	[DEPTH_TO_BEDROCK] [int] NULL,
	[DEPTH_TO_CARBONATES] [int] NULL,
	[MOISTURE_REGIME_DEPTH_CLASS] [text] NULL,
	[MOISTURE_REGIME] [text] NULL,
	[MODE_OF_DEPOSITION1] [text] NULL,
	[MODE_OF_DEPOSITION2] [text] NULL,
	[FUNCTIONAL_ROOTING_DEPTH] [int] NULL,
	[MAXIMUM_ROOTING_DEPTH] [int] NULL,
	[DEPTH_TO_ROOT_RESTRICTION] [int] NULL,
	[DEPTH_TO_WATER_TABLE] [int] NULL,
	[DEPTH_TO_COARSE_FRAGS] [int] NULL,
	[PROBLEMATIC_SITE_PROTOCOL_CLASS] [int] NULL,
	[SEEPAGE] [text] NULL,
	[SOIL_PIT_PHOTO] [text] NULL,
	[PRI_ECO] [text] NULL,
	[PRI_ECO_PCT] [int] NULL,
	[SEC_ECO] [text] NULL,
	[SEC_ECO_PCT] [int] NULL,
	[AZIMUTH] [int] NULL,
	[DISTANCE] [int] NULL,
	[SOIL_PIT_PHOTO1] [varbinary](1) NULL,
	[SOIL_PIT_PHOTO2] [varbinary](1) NULL,
	[COMMENTS] [text] NULL,
	[Created] [datetime2](7) NULL,
	[LastModified] [datetime2](7) NULL,
	[IsDeleted] [text] NULL,
	[CreatedAtServer] [datetime2](7) NULL,
	[LastModifiedAtServer] [datetime2](7) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UC_ECOSITEID] UNIQUE NONCLUSTERED 
(
	[ECOSITEID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PLOT]    Script Date: 4/14/2020 9:40:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PLOT](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PLOTID] [nvarchar](50) NULL,
	[PROJECTID] [text] NULL,
	[PLOT_TYPE] [text] NULL,
	[PLOTNUM] [text] NULL,
	[ADMINISTRATIVE] [int] NULL,
	[FOREST_DISTRICT] [text] NULL,
	[FMU] [int] NULL,
	[MANAGEMENT_UNIT] [int] NULL,
	[IMAGE_ANNOTATION] [text] NULL,
	[PLOTKEY] [text] NULL,
	[PLOT_DATE] [text] NULL,
	[MEASUREMENT_TYPE] [text] NULL,
	[LEAD_SPP] [int] NULL,
	[ORIGIN] [int] NULL,
	[CANOPY_STRUCTURE] [text] NULL,
	[MATURITY] [text] NULL,
	[CROWN_CLOSURE] [int] NULL,
	[FIELD_CREW1] [text] NULL,
	[FIELD_CREW2] [text] NULL,
	[FIELD_CREW3] [text] NULL,
	[DECLINATION] [text] NULL,
	[UTM_ZONE] [int] NULL,
	[UTM_EASTING] [float] NULL,
	[UTM_NORTHING] [float] NULL,
	[DATUM] [text] NULL,
	[COMMENTS] [text] NULL,
	[Created] [datetime2](7) NULL,
	[LastModified] [datetime2](7) NULL,
	[IsDeleted] [text] NULL,
	[CreatedAtServer] [datetime2](7) NULL,
	[LastModifiedAtServer] [datetime2](7) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UC_PLOTID] UNIQUE NONCLUSTERED 
(
	[PLOTID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PROJECT]    Script Date: 4/14/2020 9:40:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PROJECT](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PROJECTID] [nvarchar](50) NULL,
	[NAME] [text] NULL,
	[DESCRIPTION] [text] NULL,
	[PROJECT_DATE] [text] NULL,
	[Created] [datetime2](7) NULL,
	[LastModified] [datetime2](7) NULL,
	[IsDeleted] [text] NULL,
	[CreatedAtServer] [datetime2](7) NULL,
	[LastModifiedAtServer] [datetime2](7) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UC_PROJECTID] UNIQUE NONCLUSTERED 
(
	[PROJECTID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SMALLTREE]    Script Date: 4/14/2020 9:40:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SMALLTREE](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SMALLTREEID] [nvarchar](50) NULL,
	[PLOTID] [text] NULL,
	[SPECIES] [int] NULL,
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
	[IsDeleted] [text] NULL,
	[CreatedAtServer] [datetime2](7) NULL,
	[LastModifiedAtServer] [datetime2](7) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UC_SMALLTREEID] UNIQUE NONCLUSTERED 
(
	[SMALLTREEID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SOIL]    Script Date: 4/14/2020 9:40:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SOIL](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SOILID] [nvarchar](50) NULL,
	[PLOTID] [text] NULL,
	[LAYER] [int] NULL,
	[FROM] [int] NULL,
	[TO] [int] NULL,
	[HORIZON] [text] NULL,
	[VON_POST] [text] NULL,
	[TEXTURE] [text] NULL,
	[PORE_PATTERN] [text] NULL,
	[STRUCTURE] [text] NULL,
	[COLOUR] [text] NULL,
	[MOTTLE_COLOUR] [text] NULL,
	[PERCENT_GRAVEL] [int] NULL,
	[PERCENT_COBBLE] [int] NULL,
	[PERCENT_STONE] [int] NULL,
	[Created] [datetime2](7) NULL,
	[LastModified] [datetime2](7) NULL,
	[IsDeleted] [text] NULL,
	[CreatedAtServer] [datetime2](7) NULL,
	[LastModifiedAtServer] [datetime2](7) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UC_SOILID] UNIQUE NONCLUSTERED 
(
	[SOILID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[STEMMAP]    Script Date: 4/14/2020 9:40:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[STEMMAP](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[STEMMAPID] [nvarchar](50) NULL,
	[TREEID] [text] NULL,
	[AZIMUTH] [int] NULL,
	[DISTANCE] [float] NULL,
	[CROWN_AXIS_LONG] [float] NULL,
	[CROWN_AXIS_SHORT] [float] NULL,
	[OFFSET_AZIMUTH] [int] NULL,
	[OFFSET_DISTANCE] [float] NULL,
	[Created] [datetime2](7) NULL,
	[LastModified] [datetime2](7) NULL,
	[IsDeleted] [text] NULL,
	[CreatedAtServer] [datetime2](7) NULL,
	[LastModifiedAtServer] [datetime2](7) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UC_STEMMAPID] UNIQUE NONCLUSTERED 
(
	[STEMMAPID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TREE]    Script Date: 4/14/2020 9:40:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TREE](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[TREEID] [nvarchar](50) NULL,
	[PLOTID] [text] NULL,
	[SECTION] [int] NULL,
	[TREENUM] [int] NULL,
	[SPECIES] [int] NULL,
	[TAG_TYPE] [text] NULL,
	[ORIGIN] [text] NULL,
	[STATUS] [text] NULL,
	[VIGOUR] [int] NULL,
	[HT_TO_DBH] [float] NULL,
	[DBH] [float] NULL,
	[HT] [float] NULL,
	[LENGTH] [float] NULL,
	[DBH_IN] [text] NULL,
	[CROWN_IN] [text] NULL,
	[LIVE_CROWN_RATIO] [int] NULL,
	[CROWN_CLASS] [text] NULL,
	[CROWN_POSITION] [int] NULL,
	[CROWN_DAMAGE] [int] NULL,
	[DEFOLIATING_INSECT] [int] NULL,
	[FOLIAR_DISEASE] [int] NULL,
	[STEM_QUALITY] [text] NULL,
	[BARK_RETENTION] [int] NULL,
	[WOOD_CONDITION] [int] NULL,
	[DECAY_CLASS] [int] NULL,
	[MORT_CAUSE] [int] NULL,
	[BROKEN_TOP] [text] NULL,
	[AGE] [int] NULL,
	[AGE_OFFICE] [int] NULL,
	[AGE_CORE_LENGTH] [float] NULL,
	[AZIMUTH] [float] NULL,
	[DISTANCE] [float] NULL,
	[CROWN_WIDTH1] [float] NULL,
	[CROWN_WIDTH2] [float] NULL,
	[COMMENTS] [text] NULL,
	[Created] [datetime2](7) NULL,
	[LastModified] [datetime2](7) NULL,
	[IsDeleted] [text] NULL,
	[CreatedAtServer] [datetime2](7) NULL,
	[LastModifiedAtServer] [datetime2](7) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UC_TREEID] UNIQUE NONCLUSTERED 
(
	[TREEID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[VEGETATION]    Script Date: 4/14/2020 9:40:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VEGETATION](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[VEGETATIONID] [nvarchar](50) NULL,
	[PLOTID] [text] NULL,
	[SPECIES] [text] NULL,
	[QUAD1] [int] NULL,
	[QUAD2] [int] NULL,
	[QUAD3] [int] NULL,
	[QUAD4] [int] NULL,
	[ELCLAYER3] [int] NULL,
	[ELCLAYER4] [int] NULL,
	[ELCLAYER5] [int] NULL,
	[ELCLAYER6] [int] NULL,
	[ELCLAYER7] [int] NULL,
	[Created] [datetime2](7) NULL,
	[LastModified] [datetime2](7) NULL,
	[IsDeleted] [text] NULL,
	[CreatedAtServer] [datetime2](7) NULL,
	[LastModifiedAtServer] [datetime2](7) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UC_VEGETATIONID] UNIQUE NONCLUSTERED 
(
	[VEGETATIONID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[DEFORMITY] ADD  DEFAULT (getdate()) FOR [Created]
GO
ALTER TABLE [dbo].[DEFORMITY] ADD  DEFAULT (getdate()) FOR [LastModified]
GO
ALTER TABLE [dbo].[DEFORMITY] ADD  DEFAULT ('N') FOR [IsDeleted]
GO
ALTER TABLE [dbo].[DEFORMITY] ADD  DEFAULT (getdate()) FOR [CreatedAtServer]
GO
ALTER TABLE [dbo].[DEFORMITY] ADD  DEFAULT (getdate()) FOR [LastModifiedAtServer]
GO
ALTER TABLE [dbo].[DWD] ADD  DEFAULT (getdate()) FOR [Created]
GO
ALTER TABLE [dbo].[DWD] ADD  DEFAULT (getdate()) FOR [LastModified]
GO
ALTER TABLE [dbo].[DWD] ADD  DEFAULT ('N') FOR [IsDeleted]
GO
ALTER TABLE [dbo].[DWD] ADD  DEFAULT (getdate()) FOR [CreatedAtServer]
GO
ALTER TABLE [dbo].[DWD] ADD  DEFAULT (getdate()) FOR [LastModifiedAtServer]
GO
ALTER TABLE [dbo].[ECOSITE] ADD  DEFAULT (getdate()) FOR [Created]
GO
ALTER TABLE [dbo].[ECOSITE] ADD  DEFAULT (getdate()) FOR [LastModified]
GO
ALTER TABLE [dbo].[ECOSITE] ADD  DEFAULT ('N') FOR [IsDeleted]
GO
ALTER TABLE [dbo].[ECOSITE] ADD  DEFAULT (getdate()) FOR [CreatedAtServer]
GO
ALTER TABLE [dbo].[ECOSITE] ADD  DEFAULT (getdate()) FOR [LastModifiedAtServer]
GO
ALTER TABLE [dbo].[PLOT] ADD  DEFAULT (getdate()) FOR [Created]
GO
ALTER TABLE [dbo].[PLOT] ADD  DEFAULT (getdate()) FOR [LastModified]
GO
ALTER TABLE [dbo].[PLOT] ADD  DEFAULT ('N') FOR [IsDeleted]
GO
ALTER TABLE [dbo].[PLOT] ADD  DEFAULT (getdate()) FOR [CreatedAtServer]
GO
ALTER TABLE [dbo].[PLOT] ADD  DEFAULT (getdate()) FOR [LastModifiedAtServer]
GO
ALTER TABLE [dbo].[PROJECT] ADD  DEFAULT (getdate()) FOR [Created]
GO
ALTER TABLE [dbo].[PROJECT] ADD  DEFAULT (getdate()) FOR [LastModified]
GO
ALTER TABLE [dbo].[PROJECT] ADD  DEFAULT ('N') FOR [IsDeleted]
GO
ALTER TABLE [dbo].[PROJECT] ADD  DEFAULT (getdate()) FOR [CreatedAtServer]
GO
ALTER TABLE [dbo].[PROJECT] ADD  DEFAULT (getdate()) FOR [LastModifiedAtServer]
GO
ALTER TABLE [dbo].[SMALLTREE] ADD  DEFAULT (getdate()) FOR [Created]
GO
ALTER TABLE [dbo].[SMALLTREE] ADD  DEFAULT (getdate()) FOR [LastModified]
GO
ALTER TABLE [dbo].[SMALLTREE] ADD  DEFAULT ('N') FOR [IsDeleted]
GO
ALTER TABLE [dbo].[SMALLTREE] ADD  DEFAULT (getdate()) FOR [CreatedAtServer]
GO
ALTER TABLE [dbo].[SMALLTREE] ADD  DEFAULT (getdate()) FOR [LastModifiedAtServer]
GO
ALTER TABLE [dbo].[SOIL] ADD  DEFAULT (getdate()) FOR [Created]
GO
ALTER TABLE [dbo].[SOIL] ADD  DEFAULT (getdate()) FOR [LastModified]
GO
ALTER TABLE [dbo].[SOIL] ADD  DEFAULT ('N') FOR [IsDeleted]
GO
ALTER TABLE [dbo].[SOIL] ADD  DEFAULT (getdate()) FOR [CreatedAtServer]
GO
ALTER TABLE [dbo].[SOIL] ADD  DEFAULT (getdate()) FOR [LastModifiedAtServer]
GO
ALTER TABLE [dbo].[STEMMAP] ADD  DEFAULT (getdate()) FOR [Created]
GO
ALTER TABLE [dbo].[STEMMAP] ADD  DEFAULT (getdate()) FOR [LastModified]
GO
ALTER TABLE [dbo].[STEMMAP] ADD  DEFAULT ('N') FOR [IsDeleted]
GO
ALTER TABLE [dbo].[STEMMAP] ADD  DEFAULT (getdate()) FOR [CreatedAtServer]
GO
ALTER TABLE [dbo].[STEMMAP] ADD  DEFAULT (getdate()) FOR [LastModifiedAtServer]
GO
ALTER TABLE [dbo].[TREE] ADD  DEFAULT (getdate()) FOR [Created]
GO
ALTER TABLE [dbo].[TREE] ADD  DEFAULT (getdate()) FOR [LastModified]
GO
ALTER TABLE [dbo].[TREE] ADD  DEFAULT ('N') FOR [IsDeleted]
GO
ALTER TABLE [dbo].[TREE] ADD  DEFAULT (getdate()) FOR [CreatedAtServer]
GO
ALTER TABLE [dbo].[TREE] ADD  DEFAULT (getdate()) FOR [LastModifiedAtServer]
GO
ALTER TABLE [dbo].[VEGETATION] ADD  DEFAULT (getdate()) FOR [Created]
GO
ALTER TABLE [dbo].[VEGETATION] ADD  DEFAULT (getdate()) FOR [LastModified]
GO
ALTER TABLE [dbo].[VEGETATION] ADD  DEFAULT ('N') FOR [IsDeleted]
GO
ALTER TABLE [dbo].[VEGETATION] ADD  DEFAULT (getdate()) FOR [CreatedAtServer]
GO
ALTER TABLE [dbo].[VEGETATION] ADD  DEFAULT (getdate()) FOR [LastModifiedAtServer]
GO
/****** Object:  StoredProcedure [dbo].[DeleteDeformity]    Script Date: 4/14/2020 9:40:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****** Object:  StoredProcedure [dbo].[DeleteProject]    Script Date: 4/14/2020 4:07:32 PM ******/



CREATE PROCEDURE [dbo].[DeleteDeformity]  @id nvarchar(max)
AS BEGIN

BEGIN TRY
	  UPDATE DEFORMITY Set IsDeleted = 'Y' where DEFORMITYID = @id
	 RETURN 1
END TRY
BEGIN CATCH
     RETURN 0
END CATCH

END
GO
/****** Object:  StoredProcedure [dbo].[DeleteDWD]    Script Date: 4/14/2020 9:40:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****** Object:  StoredProcedure [dbo].[DeleteProject]    Script Date: 4/14/2020 4:07:32 PM ******/



CREATE PROCEDURE [dbo].[DeleteDWD]  @id nvarchar(max)
AS BEGIN

BEGIN TRY
	  UPDATE DWD Set IsDeleted = 'Y' where DWDID = @id
	 RETURN 1
END TRY
BEGIN CATCH
     RETURN 0
END CATCH

END
GO
/****** Object:  StoredProcedure [dbo].[DeleteEcosite]    Script Date: 4/14/2020 9:40:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****** Object:  StoredProcedure [dbo].[DeleteProject]    Script Date: 4/14/2020 4:07:32 PM ******/



CREATE PROCEDURE [dbo].[DeleteEcosite]  @id nvarchar(max)
AS BEGIN

BEGIN TRY
	  UPDATE ECOSITE Set IsDeleted = 'Y' where ECOSITEID = @id
	 RETURN 1
END TRY
BEGIN CATCH
     RETURN 0
END CATCH

END
GO
/****** Object:  StoredProcedure [dbo].[DeletePlot]    Script Date: 4/14/2020 9:40:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****** Object:  StoredProcedure [dbo].[DeleteProject]    Script Date: 4/14/2020 4:07:32 PM ******/



CREATE PROCEDURE [dbo].[DeletePlot]  @id nvarchar(max)
AS BEGIN

BEGIN TRY
	  UPDATE PLOT Set IsDeleted = 'Y' where PLOTID = @id
	 RETURN 1
END TRY
BEGIN CATCH
     RETURN 0
END CATCH

END
GO
/****** Object:  StoredProcedure [dbo].[DeleteProject]    Script Date: 4/14/2020 9:40:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[DeleteProject]  @id nvarchar(max)
AS BEGIN

BEGIN TRY
	  UPDATE PROJECT Set IsDeleted = 'Y' where PROJECTID = @id
	 RETURN 1
END TRY
BEGIN CATCH
     RETURN 0
END CATCH

END
GO
/****** Object:  StoredProcedure [dbo].[DeleteSmallTree]    Script Date: 4/14/2020 9:40:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****** Object:  StoredProcedure [dbo].[DeleteProject]    Script Date: 4/14/2020 4:07:32 PM ******/



CREATE PROCEDURE [dbo].[DeleteSmallTree]  @id nvarchar(max)
AS BEGIN

BEGIN TRY
	  UPDATE SMALLTREE Set IsDeleted = 'Y' where SMALLTREEID = @id
	 RETURN 1
END TRY
BEGIN CATCH
     RETURN 0
END CATCH

END
GO
/****** Object:  StoredProcedure [dbo].[DeleteSoil]    Script Date: 4/14/2020 9:40:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****** Object:  StoredProcedure [dbo].[DeleteProject]    Script Date: 4/14/2020 4:07:32 PM ******/



CREATE PROCEDURE [dbo].[DeleteSoil]  @id nvarchar(max)
AS BEGIN

BEGIN TRY
	  UPDATE SOIL Set IsDeleted = 'Y' where SOILID = @id
	 RETURN 1
END TRY
BEGIN CATCH
     RETURN 0
END CATCH

END
GO
/****** Object:  StoredProcedure [dbo].[DeleteStemmap]    Script Date: 4/14/2020 9:40:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****** Object:  StoredProcedure [dbo].[DeleteProject]    Script Date: 4/14/2020 4:07:32 PM ******/



CREATE PROCEDURE [dbo].[DeleteStemmap]  @id nvarchar(max)
AS BEGIN

BEGIN TRY
	  UPDATE STEMMAP Set IsDeleted = 'Y' where STEMMAPID = @id
	 RETURN 1
END TRY
BEGIN CATCH
     RETURN 0
END CATCH

END
GO
/****** Object:  StoredProcedure [dbo].[DeleteTree]    Script Date: 4/14/2020 9:40:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****** Object:  StoredProcedure [dbo].[DeleteProject]    Script Date: 4/14/2020 4:07:32 PM ******/



CREATE PROCEDURE [dbo].[DeleteTree]  @id nvarchar(max)
AS BEGIN

BEGIN TRY
	  UPDATE TREE Set IsDeleted = 'Y' where TREEID = @id
	 RETURN 1
END TRY
BEGIN CATCH
     RETURN 0
END CATCH

END
GO
/****** Object:  StoredProcedure [dbo].[DeleteVegetation]    Script Date: 4/14/2020 9:40:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****** Object:  StoredProcedure [dbo].[DeleteProject]    Script Date: 4/14/2020 4:07:32 PM ******/



CREATE PROCEDURE [dbo].[DeleteVegetation]  @id nvarchar(max)
AS BEGIN

BEGIN TRY
	  UPDATE VEGETATION Set IsDeleted = 'Y' where VEGETATIONID = @id
	 RETURN 1
END TRY
BEGIN CATCH
     RETURN 0
END CATCH

END
GO
/****** Object:  StoredProcedure [dbo].[InsertDeformity]    Script Date: 4/14/2020 9:40:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[InsertDeformity](@fromjson nvarchar(max))
AS BEGIN

  BEGIN TRY
    insert into DEFORMITY(DEFORMITYID,TREEID,TYPE,CAUSE,HT_FROM,HT_TO,QUAD,EXTENT,LEAN,AZIMUTH,LENGTH,WIDTH,PCT_SCUFF,PCT_SCRAPE,PCT_GOUGE,OLD_FEEDING_CAVITY,NEW_FEEDING_CAVITY,OLD_NEST_CAVITY,
NEW_NEST_CAVITY,STICK_NEST,Created,LastModified,IsDeleted)
    select DEFORMITYID,TREEID,TYPE,CAUSE,HT_FROM,HT_TO,QUAD,EXTENT,LEAN,AZIMUTH,LENGTH,WIDTH,PCT_SCUFF,PCT_SCRAPE,PCT_GOUGE,OLD_FEEDING_CAVITY,NEW_FEEDING_CAVITY,OLD_NEST_CAVITY,
NEW_NEST_CAVITY,STICK_NEST,Created,LastModified,IsDeleted
    FROM OPENJSON (@fromjson)
     WITH (
DEFORMITYID nvarchar(50),
TREEID nvarchar(max),
TYPE int,
CAUSE int,
HT_FROM float,
HT_TO float,
QUAD nvarchar(max),
EXTENT int,
LEAN int,
AZIMUTH int,
LENGTH float,
WIDTH float,
PCT_SCUFF int,
PCT_SCRAPE int,
PCT_GOUGE int,
OLD_FEEDING_CAVITY nvarchar(max),
NEW_FEEDING_CAVITY nvarchar(max),
OLD_NEST_CAVITY nvarchar(max),
NEW_NEST_CAVITY nvarchar(max),
STICK_NEST nvarchar(max),
Created datetime2,
LastModified datetime2,
IsDeleted nvarchar(max)
)
	 RETURN 1
  END TRY
  BEGIN CATCH
     RETURN 0
  END CATCH

END
GO
/****** Object:  StoredProcedure [dbo].[InsertDWD]    Script Date: 4/14/2020 9:40:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[InsertDWD](@fromjson nvarchar(max))
AS BEGIN

  BEGIN TRY
    insert into DWD(DWDID, 	PLOTID, LINE, DWDNUM, SPECIES, DIAM, DECOMP_CLASS, ORIGIN, TILT_ANGLE, LENGTH, SMALL_DIAM, LARGE_DIAM, GT_50_MOSS, BURNED, HOLLOW, IS_ACCUM, ACCUM_LENGTH, ACCUM_DEPTH, PERCENT_CONIFER,
PERCENT_DECID, Created, LastModified, IsDeleted)
    select DWDID, 	PLOTID, LINE, DWDNUM, SPECIES, DIAM, DECOMP_CLASS, ORIGIN, TILT_ANGLE, LENGTH, SMALL_DIAM, LARGE_DIAM, GT_50_MOSS, BURNED, HOLLOW, IS_ACCUM, ACCUM_LENGTH, ACCUM_DEPTH, PERCENT_CONIFER,
PERCENT_DECID, Created, LastModified, IsDeleted
    FROM OPENJSON (@fromjson)
     WITH (
            DWDID nvarchar(50),
	PLOTID nvarchar(max) ,
	LINE int,
	DWDNUM int,
	SPECIES int,
	DIAM float,
	DECOMP_CLASS int,
	ORIGIN nvarchar(max),
	TILT_ANGLE int,
	[LENGTH] float,
	SMALL_DIAM float,
	LARGE_DIAM float,
	GT_50_MOSS nvarchar(max),
	BURNED nvarchar(max),
	HOLLOW nvarchar(max),
	IS_ACCUM nvarchar(max),
	ACCUM_LENGTH float,
	ACCUM_DEPTH float,
	PERCENT_CONIFER int,
	PERCENT_DECID int,
	Created datetime2,
	LastModified datetime2,
	IsDeleted nvarchar(max)
     )
	 RETURN 1
  END TRY
  BEGIN CATCH
     RETURN 0
  END CATCH

END
GO
/****** Object:  StoredProcedure [dbo].[InsertEcosite]    Script Date: 4/14/2020 9:40:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[InsertEcosite](@fromjson nvarchar(max))
AS BEGIN

  BEGIN TRY
    insert into ECOSITE(ECOSITEID, PLOTID, HUMUS_FORM, DRAINAGE, STRATIFIED, EFFECTIVE_PORE_PATTERN, ELC_SUBSTRATE_TYPE,DEPTH_TO_DISTINCT_MOTTLES, DEPTH_TO_PROMINENT_MOTTLES, DEPTH_TO_GLEY, DEPTH_TO_BEDROCK,
DEPTH_TO_CARBONATES, MOISTURE_REGIME_DEPTH_CLASS, MOISTURE_REGIME, MODE_OF_DEPOSITION1, MODE_OF_DEPOSITION2, FUNCTIONAL_ROOTING_DEPTH, MAXIMUM_ROOTING_DEPTH, DEPTH_TO_ROOT_RESTRICTION,
DEPTH_TO_WATER_TABLE, DEPTH_TO_COARSE_FRAGS, PROBLEMATIC_SITE_PROTOCOL_CLASS, SEEPAGE, SOIL_PIT_PHOTO, PRI_ECO, PRI_ECO_PCT, SEC_ECO,SEC_ECO_PCT,AZIMUTH,DISTANCE,SOIL_PIT_PHOTO1,
SOIL_PIT_PHOTO2, COMMENTS, Created, LastModified,IsDeleted)
    select ECOSITEID, PLOTID, HUMUS_FORM, DRAINAGE, STRATIFIED, EFFECTIVE_PORE_PATTERN, ELC_SUBSTRATE_TYPE,DEPTH_TO_DISTINCT_MOTTLES, DEPTH_TO_PROMINENT_MOTTLES, DEPTH_TO_GLEY, DEPTH_TO_BEDROCK,
DEPTH_TO_CARBONATES, MOISTURE_REGIME_DEPTH_CLASS, MOISTURE_REGIME, MODE_OF_DEPOSITION1, MODE_OF_DEPOSITION2, FUNCTIONAL_ROOTING_DEPTH, MAXIMUM_ROOTING_DEPTH, DEPTH_TO_ROOT_RESTRICTION,
DEPTH_TO_WATER_TABLE, DEPTH_TO_COARSE_FRAGS, PROBLEMATIC_SITE_PROTOCOL_CLASS, SEEPAGE, SOIL_PIT_PHOTO, PRI_ECO, PRI_ECO_PCT, SEC_ECO,SEC_ECO_PCT,AZIMUTH,DISTANCE,SOIL_PIT_PHOTO1,
SOIL_PIT_PHOTO2, COMMENTS, Created, LastModified,IsDeleted
    FROM OPENJSON (@fromjson)
     WITH (
    ECOSITEID nvarchar(50),
	PLOTID nvarchar(max),
	HUMUS_FORM int,
	DRAINAGE int,
	STRATIFIED nvarchar(max),
	EFFECTIVE_PORE_PATTERN nvarchar(max),
	ELC_SUBSTRATE_TYPE nvarchar(max),
	DEPTH_TO_DISTINCT_MOTTLES int,
	DEPTH_TO_PROMINENT_MOTTLES int,
	DEPTH_TO_GLEY int,
	DEPTH_TO_BEDROCK int,
	DEPTH_TO_CARBONATES int,
	MOISTURE_REGIME_DEPTH_CLASS nvarchar(max),
	MOISTURE_REGIME nvarchar(max),
	MODE_OF_DEPOSITION1 nvarchar(max),
	MODE_OF_DEPOSITION2 nvarchar(max),
	FUNCTIONAL_ROOTING_DEPTH int,
	MAXIMUM_ROOTING_DEPTH int,
	DEPTH_TO_ROOT_RESTRICTION int,
	DEPTH_TO_WATER_TABLE int,
	DEPTH_TO_COARSE_FRAGS int,
	PROBLEMATIC_SITE_PROTOCOL_CLASS int,
	SEEPAGE nvarchar(max),
	SOIL_PIT_PHOTO nvarchar(max),
	PRI_ECO nvarchar(max),
	PRI_ECO_PCT int,
	SEC_ECO nvarchar(max),
	SEC_ECO_PCT int,
	AZIMUTH int,
	DISTANCE int,
	SOIL_PIT_PHOTO1 varbinary(1),
	SOIL_PIT_PHOTO2 varbinary(1),
	COMMENTS nvarchar(max),
	Created datetime2,
	LastModified datetime2,
	IsDeleted nvarchar(max)
     )
	 RETURN 1
  END TRY
  BEGIN CATCH
     RETURN 0
  END CATCH

END
GO
/****** Object:  StoredProcedure [dbo].[InsertPlot]    Script Date: 4/14/2020 9:40:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[InsertPlot](@fromjson nvarchar(max))
AS BEGIN

  BEGIN TRY
    insert into PLOT(PLOTID,	PROJECTID,PLOT_TYPE,PLOTNUM,ADMINISTRATIVE,FOREST_DISTRICT,FMU,MANAGEMENT_UNIT,IMAGE_ANNOTATION,PLOTKEY,PLOT_DATE,MEASUREMENT_TYPE,LEAD_SPP,ORIGIN,CANOPY_STRUCTURE,
MATURITY,CROWN_CLOSURE,	FIELD_CREW1,FIELD_CREW2,FIELD_CREW3,DECLINATION, UTM_ZONE, UTM_EASTING, UTM_NORTHING,DATUM, COMMENTS, Created,LastModified,IsDeleted)
    select PLOTID,	PROJECTID,PLOT_TYPE,PLOTNUM,ADMINISTRATIVE,FOREST_DISTRICT,FMU,MANAGEMENT_UNIT,IMAGE_ANNOTATION,PLOTKEY,PLOT_DATE,MEASUREMENT_TYPE,LEAD_SPP,ORIGIN,CANOPY_STRUCTURE,
MATURITY,CROWN_CLOSURE,	FIELD_CREW1,FIELD_CREW2,FIELD_CREW3,DECLINATION, UTM_ZONE, UTM_EASTING, UTM_NORTHING,DATUM, COMMENTS, Created,LastModified,IsDeleted
    FROM OPENJSON (@fromjson)
     WITH (
   PLOTID nvarchar(50),
	PROJECTID nvarchar(max),
	PLOT_TYPE nvarchar(max),
	PLOTNUM nvarchar(max),
	ADMINISTRATIVE int,
	FOREST_DISTRICT nvarchar(max),
	FMU int,
	MANAGEMENT_UNIT int,
	IMAGE_ANNOTATION nvarchar(max),
	PLOTKEY nvarchar(max),
	PLOT_DATE nvarchar(max),
	MEASUREMENT_TYPE nvarchar(max),
	LEAD_SPP int,
	ORIGIN int,
	CANOPY_STRUCTURE nvarchar(max),
	MATURITY nvarchar(max),
	CROWN_CLOSURE int,
	FIELD_CREW1 nvarchar(max),
	FIELD_CREW2 nvarchar(max),
	FIELD_CREW3 nvarchar(max),
	DECLINATION nvarchar(max),
	UTM_ZONE int,
	UTM_EASTING float,
	UTM_NORTHING float,
	DATUM nvarchar(max),
	COMMENTS nvarchar(max),
	Created datetime2,
	LastModified datetime2,
	IsDeleted nvarchar(max)
     )
	 RETURN 1
  END TRY
  BEGIN CATCH
     RETURN 0
  END CATCH

END
GO
/****** Object:  StoredProcedure [dbo].[InsertProject]    Script Date: 4/14/2020 9:40:00 PM ******/
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
/****** Object:  StoredProcedure [dbo].[InsertSmalltree]    Script Date: 4/14/2020 9:40:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[InsertSmalltree](@fromjson nvarchar(max))
AS BEGIN

  BEGIN TRY
    insert into SMALLTREE(SMALLTREEID,PLOTID,SPECIES,HT_CLASS1_COUNT,HT_CLASS2_COUNT,HT_CLASS3_COUNT,HT_CLASS4_COUNT,HT_CLASS5_COUNT,HT_CLASS6_COUNT,HT_CLASS7_COUNT,HT_CLASS8_COUNT,
Created,LastModified,IsDeleted)
    select SMALLTREEID,
PLOTID,SPECIES,HT_CLASS1_COUNT,HT_CLASS2_COUNT,HT_CLASS3_COUNT,HT_CLASS4_COUNT,HT_CLASS5_COUNT,HT_CLASS6_COUNT,HT_CLASS7_COUNT,HT_CLASS8_COUNT,Created,LastModified,IsDeleted
    FROM OPENJSON (@fromjson)
     WITH (
  SMALLTREEID nvarchar(50),
PLOTID nvarchar(max),
SPECIES int,
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
IsDeleted nvarchar(max)
     )
	 RETURN 1
  END TRY
  BEGIN CATCH
     RETURN 0
  END CATCH

END
GO
/****** Object:  StoredProcedure [dbo].[InsertSoil]    Script Date: 4/14/2020 9:40:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[InsertSoil](@fromjson nvarchar(max))
AS BEGIN

  BEGIN TRY
    insert into SOIL(SOILID,PLOTID,LAYER,[FROM],[TO],HORIZON,VON_POST,TEXTURE,PORE_PATTERN,STRUCTURE,COLOUR,MOTTLE_COLOUR,PERCENT_GRAVEL,PERCENT_COBBLE,PERCENT_STONE,Created,LastModified,IsDeleted)
    select SOILID,PLOTID,LAYER,[FROM],[TO],HORIZON,VON_POST,TEXTURE,PORE_PATTERN,STRUCTURE,COLOUR,MOTTLE_COLOUR,PERCENT_GRAVEL,PERCENT_COBBLE,PERCENT_STONE,Created,LastModified,IsDeleted
    FROM OPENJSON (@fromjson)
     WITH (
SOILID nvarchar(max),
PLOTID nvarchar(max),
LAYER int,
[FROM] int,
[TO] int,
HORIZON nvarchar(max),
VON_POST nvarchar(max),
TEXTURE nvarchar(max),
PORE_PATTERN nvarchar(max),
STRUCTURE nvarchar(max),
COLOUR nvarchar(max),
MOTTLE_COLOUR nvarchar(max),
PERCENT_GRAVEL int,
PERCENT_COBBLE int,
PERCENT_STONE int,
Created datetime2,
LastModified datetime2,
IsDeleted nvarchar(max)
     )
	 RETURN 1
  END TRY
  BEGIN CATCH
     RETURN 0
  END CATCH

END
GO
/****** Object:  StoredProcedure [dbo].[InsertStemmap]    Script Date: 4/14/2020 9:40:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[InsertStemmap](@fromjson nvarchar(max))
AS BEGIN

  BEGIN TRY
    insert into STEMMAP(STEMMAPID,TREEID,AZIMUTH,DISTANCE,CROWN_AXIS_LONG,CROWN_AXIS_SHORT,OFFSET_AZIMUTH,OFFSET_DISTANCE,Created,LastModified,IsDeleted)
    select STEMMAPID,TREEID,AZIMUTH,DISTANCE,CROWN_AXIS_LONG,CROWN_AXIS_SHORT,OFFSET_AZIMUTH,OFFSET_DISTANCE,Created,LastModified,IsDeleted
    FROM OPENJSON (@fromjson)
     WITH (
STEMMAPID nvarchar(50),
TREEID nvarchar(max),
AZIMUTH int,
DISTANCE float,
CROWN_AXIS_LONG float,
CROWN_AXIS_SHORT float,
OFFSET_AZIMUTH int,
OFFSET_DISTANCE float,
Created datetime2,
LastModified datetime2,
IsDeleted nvarchar(max)
     )
	 RETURN 1
  END TRY
  BEGIN CATCH
     RETURN 0
  END CATCH

END
GO
/****** Object:  StoredProcedure [dbo].[InsertTree]    Script Date: 4/14/2020 9:40:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[InsertTree](@fromjson nvarchar(max))
AS BEGIN

  BEGIN TRY
    insert into TREE(TREEID,PLOTID,SECTION,TREENUM,SPECIES,TAG_TYPE,ORIGIN,STATUS,VIGOUR,HT_TO_DBH,DBH,HT,LENGTH,DBH_IN,CROWN_IN,LIVE_CROWN_RATIO,CROWN_CLASS,CROWN_POSITION,CROWN_DAMAGE,DEFOLIATING_INSECT,
FOLIAR_DISEASE,STEM_QUALITY,BARK_RETENTION,WOOD_CONDITION,DECAY_CLASS,MORT_CAUSE,BROKEN_TOP,AGE,AGE_OFFICE,AGE_CORE_LENGTH,AZIMUTH,DISTANCE,CROWN_WIDTH1,CROWN_WIDTH2,COMMENTS,
Created,LastModified,IsDeleted)
    select TREEID,PLOTID,SECTION,TREENUM,SPECIES,TAG_TYPE,ORIGIN,STATUS,VIGOUR,HT_TO_DBH,DBH,HT,LENGTH,DBH_IN,CROWN_IN,LIVE_CROWN_RATIO,CROWN_CLASS,CROWN_POSITION,CROWN_DAMAGE,DEFOLIATING_INSECT,
FOLIAR_DISEASE,STEM_QUALITY,BARK_RETENTION,WOOD_CONDITION,DECAY_CLASS,MORT_CAUSE,BROKEN_TOP,AGE,AGE_OFFICE,AGE_CORE_LENGTH,AZIMUTH,DISTANCE,CROWN_WIDTH1,CROWN_WIDTH2,COMMENTS,
Created,LastModified,IsDeleted
    FROM OPENJSON (@fromjson)
     WITH (
TREEID nvarchar(50),
PLOTID nvarchar(max),
SECTION int,
TREENUM int,
SPECIES int,
TAG_TYPE nvarchar(max),
ORIGIN nvarchar(max),
STATUS nvarchar(max),
VIGOUR int,
HT_TO_DBH float,
DBH float,
HT float,
LENGTH float,
DBH_IN nvarchar(max),
CROWN_IN nvarchar(max),
LIVE_CROWN_RATIO int,
CROWN_CLASS nvarchar(max),
CROWN_POSITION int,
CROWN_DAMAGE int,
DEFOLIATING_INSECT int,
FOLIAR_DISEASE int,
STEM_QUALITY nvarchar(max),
BARK_RETENTION int,
WOOD_CONDITION int,
DECAY_CLASS int,
MORT_CAUSE int,
BROKEN_TOP nvarchar(max),
AGE int,
AGE_OFFICE int,
AGE_CORE_LENGTH float,
AZIMUTH float,
DISTANCE float,
CROWN_WIDTH1 float,
CROWN_WIDTH2 float,
COMMENTS nvarchar(max),
Created datetime2,
LastModified datetime2,
IsDeleted nvarchar(max)
     )
	 RETURN 1
  END TRY
  BEGIN CATCH
     RETURN 0
  END CATCH

END
GO
/****** Object:  StoredProcedure [dbo].[InsertVegetation]    Script Date: 4/14/2020 9:40:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[InsertVegetation](@fromjson nvarchar(max))
AS BEGIN

  BEGIN TRY
    insert into VEGETATION(VEGETATIONID,PLOTID,SPECIES,QUAD1,QUAD2,QUAD3,QUAD4,ELCLAYER3,ELCLAYER4,ELCLAYER5,ELCLAYER6,ELCLAYER7,Created,LastModified,IsDeleted)
    select VEGETATIONID,PLOTID,SPECIES,QUAD1,QUAD2,QUAD3,QUAD4,ELCLAYER3,ELCLAYER4,ELCLAYER5,ELCLAYER6,ELCLAYER7,Created,LastModified,IsDeleted
    FROM OPENJSON (@fromjson)
     WITH (
VEGETATIONID nvarchar(50),
PLOTID nvarchar(max),
SPECIES nvarchar(max),
QUAD1 int,
QUAD2 int,
QUAD3 int,
QUAD4 int,
ELCLAYER3 int,
ELCLAYER4 int,
ELCLAYER5 int,
ELCLAYER6 int,
ELCLAYER7 int,
Created datetime2,
LastModified datetime2,
IsDeleted nvarchar(max)
)
	 RETURN 1
  END TRY
  BEGIN CATCH
     RETURN 0
  END CATCH

END
GO
/****** Object:  StoredProcedure [dbo].[UpdateDeformity]    Script Date: 4/14/2020 9:40:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/****** Object:  StoredProcedure [dbo].[InsertProject]    Script Date: 4/13/2020 2:58:46 PM ******/

CREATE PROCEDURE [dbo].[UpdateDeformity](@fromjson nvarchar(max))
AS BEGIN

  BEGIN TRY
 
    with json_data as (select DEFORMITYID,TREEID,TYPE,CAUSE,HT_FROM,HT_TO,QUAD,EXTENT,LEAN,AZIMUTH,LENGTH,WIDTH,PCT_SCUFF,PCT_SCRAPE,PCT_GOUGE,OLD_FEEDING_CAVITY,NEW_FEEDING_CAVITY,OLD_NEST_CAVITY,
NEW_NEST_CAVITY,STICK_NEST,Created,LastModified,IsDeleted
    FROM OPENJSON (@fromjson) 
    WITH (
DEFORMITYID nvarchar(50),
TREEID nvarchar(max),
TYPE int,
CAUSE int,
HT_FROM float,
HT_TO float,
QUAD nvarchar(max),
EXTENT int,
LEAN int,
AZIMUTH int,
LENGTH float,
WIDTH float,
PCT_SCUFF int,
PCT_SCRAPE int,
PCT_GOUGE int,
OLD_FEEDING_CAVITY nvarchar(max),
NEW_FEEDING_CAVITY nvarchar(max),
OLD_NEST_CAVITY nvarchar(max),
NEW_NEST_CAVITY nvarchar(max),
STICK_NEST nvarchar(max),
Created datetime2,
LastModified datetime2,
IsDeleted nvarchar(max)
    ))
    update u SET 
u.DEFORMITYID=jd.DEFORMITYID,
u.TREEID=jd.TREEID,
u.TYPE=jd.TYPE,
u.CAUSE=jd.CAUSE,
u.HT_FROM=jd.HT_FROM,
u.HT_TO=jd.HT_TO,
u.QUAD=jd.QUAD,
u.EXTENT=jd.EXTENT,
u.LEAN=jd.LEAN,
u.AZIMUTH=jd.AZIMUTH,
u.LENGTH=jd.LENGTH,
u.WIDTH=jd.WIDTH,
u.PCT_SCUFF=jd.PCT_SCUFF,
u.PCT_SCRAPE=jd.PCT_SCRAPE,
u.PCT_GOUGE=jd.PCT_GOUGE,
u.OLD_FEEDING_CAVITY=jd.OLD_FEEDING_CAVITY,
u.NEW_FEEDING_CAVITY=jd.NEW_FEEDING_CAVITY,
u.OLD_NEST_CAVITY=jd.OLD_NEST_CAVITY,
u.NEW_NEST_CAVITY=jd.NEW_NEST_CAVITY,
u.STICK_NEST=jd.STICK_NEST,
u.Created=jd.Created,
u.LastModified=jd.LastModified,
u.IsDeleted=jd.IsDeleted,
u.LastModifiedAtServer = CURRENT_TIMESTAMP
	from DEFORMITY as u
	inner join json_data as jd on jd.DEFORMITYID = u.DEFORMITYID
  
    RETURN 1
  END TRY
  BEGIN CATCH
     RETURN 0
  END CATCH

END
GO
/****** Object:  StoredProcedure [dbo].[UpdateDWD]    Script Date: 4/14/2020 9:40:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/****** Object:  StoredProcedure [dbo].[InsertProject]    Script Date: 4/13/2020 2:58:46 PM ******/

CREATE PROCEDURE [dbo].[UpdateDWD](@fromjson nvarchar(max))
AS BEGIN

  BEGIN TRY
 
    with json_data as (select DWDID, 	PLOTID, LINE, DWDNUM, SPECIES, DIAM, DECOMP_CLASS, ORIGIN, TILT_ANGLE, LENGTH, SMALL_DIAM, LARGE_DIAM, GT_50_MOSS, BURNED, HOLLOW, IS_ACCUM, ACCUM_LENGTH, ACCUM_DEPTH, PERCENT_CONIFER,
PERCENT_DECID, Created, LastModified, IsDeleted 
    FROM OPENJSON (@fromjson) 
    WITH (
            DWDID nvarchar(50),
	PLOTID nvarchar(max) ,
	LINE int,
	DWDNUM int,
	SPECIES int,
	DIAM float,
	DECOMP_CLASS int,
	ORIGIN nvarchar(max),
	TILT_ANGLE int,
	[LENGTH] float,
	SMALL_DIAM float,
	LARGE_DIAM float,
	GT_50_MOSS nvarchar(max),
	BURNED nvarchar(max),
	HOLLOW nvarchar(max),
	IS_ACCUM nvarchar(max),
	ACCUM_LENGTH float,
	ACCUM_DEPTH float,
	PERCENT_CONIFER int,
	PERCENT_DECID int,
	Created datetime2,
	LastModified datetime2,
	IsDeleted nvarchar(max)
    ))
    update u SET 
	u.DWDID=jd.DWDID,
u.PLOTID=jd.PLOTID,
u.LINE=jd.LINE,
u.DWDNUM=jd.DWDNUM,
u.SPECIES=jd.SPECIES,
u.DIAM=jd.DIAM,
u.DECOMP_CLASS=jd.DECOMP_CLASS,
u.ORIGIN=jd.ORIGIN,
u.TILT_ANGLE=jd.TILT_ANGLE,
u.LENGTH=jd.LENGTH,
u.SMALL_DIAM=jd.SMALL_DIAM,
u.LARGE_DIAM=jd.LARGE_DIAM,
u.GT_50_MOSS=jd.GT_50_MOSS,
u.BURNED=jd.BURNED,
u.HOLLOW=jd.HOLLOW,
u.IS_ACCUM=jd.IS_ACCUM,
u.ACCUM_LENGTH=jd.ACCUM_LENGTH,
u.ACCUM_DEPTH=jd.ACCUM_DEPTH,
u.PERCENT_CONIFER=jd.PERCENT_CONIFER,
u.PERCENT_DECID=jd.PERCENT_DECID,
u.Created=jd.Created,
u.LastModified=jd.LastModified,
u.IsDeleted=jd.IsDeleted,
	u.LastModifiedAtServer = CURRENT_TIMESTAMP
	from DWD as u
	inner join json_data as jd on jd.DWDID = u.DWDID
   
    RETURN 1
  END TRY
  BEGIN CATCH
     RETURN 0
  END CATCH

END
GO
/****** Object:  StoredProcedure [dbo].[UpdateEcosite]    Script Date: 4/14/2020 9:40:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/****** Object:  StoredProcedure [dbo].[InsertProject]    Script Date: 4/13/2020 2:58:46 PM ******/

CREATE PROCEDURE [dbo].[UpdateEcosite](@fromjson nvarchar(max))
AS BEGIN

  BEGIN TRY
 
    with json_data as (select ECOSITEID, PLOTID, HUMUS_FORM, DRAINAGE, STRATIFIED, EFFECTIVE_PORE_PATTERN, ELC_SUBSTRATE_TYPE,DEPTH_TO_DISTINCT_MOTTLES, DEPTH_TO_PROMINENT_MOTTLES, DEPTH_TO_GLEY, DEPTH_TO_BEDROCK,
DEPTH_TO_CARBONATES, MOISTURE_REGIME_DEPTH_CLASS, MOISTURE_REGIME, MODE_OF_DEPOSITION1, MODE_OF_DEPOSITION2, FUNCTIONAL_ROOTING_DEPTH, MAXIMUM_ROOTING_DEPTH, DEPTH_TO_ROOT_RESTRICTION,
DEPTH_TO_WATER_TABLE, DEPTH_TO_COARSE_FRAGS, PROBLEMATIC_SITE_PROTOCOL_CLASS, SEEPAGE, SOIL_PIT_PHOTO, PRI_ECO, PRI_ECO_PCT, SEC_ECO,SEC_ECO_PCT,AZIMUTH,DISTANCE,SOIL_PIT_PHOTO1,
SOIL_PIT_PHOTO2, COMMENTS, Created, LastModified,IsDeleted
    FROM OPENJSON (@fromjson) 
    WITH (
     ECOSITEID nvarchar(50),
	PLOTID nvarchar(max),
	HUMUS_FORM int,
	DRAINAGE int,
	STRATIFIED nvarchar(max),
	EFFECTIVE_PORE_PATTERN nvarchar(max),
	ELC_SUBSTRATE_TYPE nvarchar(max),
	DEPTH_TO_DISTINCT_MOTTLES int,
	DEPTH_TO_PROMINENT_MOTTLES int,
	DEPTH_TO_GLEY int,
	DEPTH_TO_BEDROCK int,
	DEPTH_TO_CARBONATES int,
	MOISTURE_REGIME_DEPTH_CLASS nvarchar(max),
	MOISTURE_REGIME nvarchar(max),
	MODE_OF_DEPOSITION1 nvarchar(max),
	MODE_OF_DEPOSITION2 nvarchar(max),
	FUNCTIONAL_ROOTING_DEPTH int,
	MAXIMUM_ROOTING_DEPTH int,
	DEPTH_TO_ROOT_RESTRICTION int,
	DEPTH_TO_WATER_TABLE int,
	DEPTH_TO_COARSE_FRAGS int,
	PROBLEMATIC_SITE_PROTOCOL_CLASS int,
	SEEPAGE nvarchar(max),
	SOIL_PIT_PHOTO nvarchar(max),
	PRI_ECO nvarchar(max),
	PRI_ECO_PCT int,
	SEC_ECO nvarchar(max),
	SEC_ECO_PCT int,
	AZIMUTH int,
	DISTANCE int,
	SOIL_PIT_PHOTO1 varbinary(1),
	SOIL_PIT_PHOTO2 varbinary(1),
	COMMENTS nvarchar(max),
	Created datetime2,
	LastModified datetime2,
	IsDeleted nvarchar(max)
    ))
    update u SET 
u.ECOSITEID=jd.ECOSITEID,
u.PLOTID=jd.PLOTID,
u.HUMUS_FORM=jd.HUMUS_FORM,
u.DRAINAGE=jd.DRAINAGE,
u.STRATIFIED =jd.STRATIFIED ,
u.EFFECTIVE_PORE_PATTERN=jd.EFFECTIVE_PORE_PATTERN,
u.ELC_SUBSTRATE_TYPE=jd.ELC_SUBSTRATE_TYPE,
u.DEPTH_TO_DISTINCT_MOTTLES=jd.DEPTH_TO_DISTINCT_MOTTLES,
u.DEPTH_TO_PROMINENT_MOTTLES=jd.DEPTH_TO_PROMINENT_MOTTLES,
u.DEPTH_TO_GLEY=jd.DEPTH_TO_GLEY,
u.DEPTH_TO_BEDROCK=jd.DEPTH_TO_BEDROCK,
u.DEPTH_TO_CARBONATES=jd.DEPTH_TO_CARBONATES,
u.MOISTURE_REGIME_DEPTH_CLASS=jd.MOISTURE_REGIME_DEPTH_CLASS,
u.MOISTURE_REGIME=jd.MOISTURE_REGIME,
u.MODE_OF_DEPOSITION1=jd.MODE_OF_DEPOSITION1,
u.MODE_OF_DEPOSITION2=jd.MODE_OF_DEPOSITION2,
u.FUNCTIONAL_ROOTING_DEPTH=jd.FUNCTIONAL_ROOTING_DEPTH,
u.MAXIMUM_ROOTING_DEPTH=jd.MAXIMUM_ROOTING_DEPTH,
u.DEPTH_TO_ROOT_RESTRICTION=jd.DEPTH_TO_ROOT_RESTRICTION,
u.DEPTH_TO_WATER_TABLE=jd.DEPTH_TO_WATER_TABLE,
u.DEPTH_TO_COARSE_FRAGS=jd.DEPTH_TO_COARSE_FRAGS,
u.PROBLEMATIC_SITE_PROTOCOL_CLASS=jd.PROBLEMATIC_SITE_PROTOCOL_CLASS,
u.SEEPAGE=jd.SEEPAGE,
u.SOIL_PIT_PHOTO=jd.SOIL_PIT_PHOTO,
u.PRI_ECO=jd.PRI_ECO,
u.PRI_ECO_PCT=jd.PRI_ECO_PCT,
u.SEC_ECO=jd.SEC_ECO,
u.SEC_ECO_PCT=jd.SEC_ECO_PCT,
u.AZIMUTH=jd.AZIMUTH,
u.DISTANCE=jd.DISTANCE,
u.SOIL_PIT_PHOTO1=jd.SOIL_PIT_PHOTO1,
u.SOIL_PIT_PHOTO2=jd.SOIL_PIT_PHOTO2,
u.COMMENTS=jd.COMMENTS,
u.Created=jd.Created,
u.LastModified=jd.LastModified,
u.IsDeleted=jd.IsDeleted,
	u.LastModifiedAtServer = CURRENT_TIMESTAMP
	from ECOSITE as u
	inner join json_data as jd on jd.ECOSITEID = u.ECOSITEID
   
    RETURN 1
  END TRY
  BEGIN CATCH
     RETURN 0
  END CATCH

END
GO
/****** Object:  StoredProcedure [dbo].[UpdatePlot]    Script Date: 4/14/2020 9:40:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/****** Object:  StoredProcedure [dbo].[InsertProject]    Script Date: 4/13/2020 2:58:46 PM ******/

CREATE PROCEDURE [dbo].[UpdatePlot](@fromjson nvarchar(max))
AS BEGIN

  BEGIN TRY
 
    with json_data as (select PLOTID,PROJECTID,PLOT_TYPE,PLOTNUM,ADMINISTRATIVE,FOREST_DISTRICT,FMU,MANAGEMENT_UNIT,IMAGE_ANNOTATION,PLOTKEY,PLOT_DATE,MEASUREMENT_TYPE,LEAD_SPP,ORIGIN,CANOPY_STRUCTURE,
MATURITY,CROWN_CLOSURE,	FIELD_CREW1,FIELD_CREW2,FIELD_CREW3,DECLINATION, UTM_ZONE, UTM_EASTING, UTM_NORTHING,DATUM, COMMENTS, Created,LastModified,IsDeleted
    FROM OPENJSON (@fromjson) 
    WITH (
PLOTID nvarchar(50),
	PROJECTID nvarchar(max),
	PLOT_TYPE nvarchar(max),
	PLOTNUM nvarchar(max),
	ADMINISTRATIVE int,
	FOREST_DISTRICT nvarchar(max),
	FMU int,
	MANAGEMENT_UNIT int,
	IMAGE_ANNOTATION nvarchar(max),
	PLOTKEY nvarchar(max),
	PLOT_DATE nvarchar(max),
	MEASUREMENT_TYPE nvarchar(max),
	LEAD_SPP int,
	ORIGIN int,
	CANOPY_STRUCTURE nvarchar(max),
	MATURITY nvarchar(max),
	CROWN_CLOSURE int,
	FIELD_CREW1 nvarchar(max),
	FIELD_CREW2 nvarchar(max),
	FIELD_CREW3 nvarchar(max),
	DECLINATION nvarchar(max),
	UTM_ZONE int,
	UTM_EASTING float,
	UTM_NORTHING float,
	DATUM nvarchar(max),
	COMMENTS nvarchar(max),
	Created datetime2,
	LastModified datetime2,
	IsDeleted nvarchar(max)
    ))
    update u SET 
u.PLOTID=jd.PLOTID,
u.PROJECTID=jd.PROJECTID,
u.PLOT_TYPE=jd.PLOT_TYPE,
u.PLOTNUM=jd.PLOTNUM,
u.ADMINISTRATIVE=jd.ADMINISTRATIVE,
u.FOREST_DISTRICT=jd.FOREST_DISTRICT,
u.FMU=jd.FMU,
u.MANAGEMENT_UNIT=jd.MANAGEMENT_UNIT,
u.IMAGE_ANNOTATION=jd.IMAGE_ANNOTATION,
u.PLOTKEY=jd.PLOTKEY,
u.PLOT_DATE=jd.PLOT_DATE,
u.MEASUREMENT_TYPE=jd.MEASUREMENT_TYPE,
u.LEAD_SPP=jd.LEAD_SPP,
u.ORIGIN=jd.ORIGIN,
u.CANOPY_STRUCTURE=jd.CANOPY_STRUCTURE,
u.MATURITY=jd.MATURITY,
u.CROWN_CLOSURE=jd.CROWN_CLOSURE,
u.FIELD_CREW1=jd.FIELD_CREW1,
u.FIELD_CREW2=jd.FIELD_CREW2,
u.FIELD_CREW3=jd.FIELD_CREW3,
u.DECLINATION=jd.DECLINATION,
u.UTM_ZONE=jd.UTM_ZONE,
u.UTM_EASTING=jd.UTM_EASTING,
u.UTM_NORTHING=jd.UTM_NORTHING,
u.DATUM=jd.DATUM,
u.COMMENTS=jd.COMMENTS,
u.Created=jd.Created,
u.LastModified=jd.LastModified,
u.IsDeleted=jd.IsDeleted,
	u.LastModifiedAtServer = CURRENT_TIMESTAMP
	from PLOT as u
	inner join json_data as jd on jd.PLOTID = u.PLOTID
   
    RETURN 1
  END TRY
  BEGIN CATCH
     RETURN 0
  END CATCH

END
GO
/****** Object:  StoredProcedure [dbo].[UpdateProject]    Script Date: 4/14/2020 9:40:00 PM ******/
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
/****** Object:  StoredProcedure [dbo].[UpdateSmalltree]    Script Date: 4/14/2020 9:40:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/****** Object:  StoredProcedure [dbo].[InsertProject]    Script Date: 4/13/2020 2:58:46 PM ******/

CREATE PROCEDURE [dbo].[UpdateSmalltree](@fromjson nvarchar(max))
AS BEGIN

  BEGIN TRY
 
    with json_data as (select SMALLTREEID,PLOTID,SPECIES,HT_CLASS1_COUNT,HT_CLASS2_COUNT,HT_CLASS3_COUNT,HT_CLASS4_COUNT,HT_CLASS5_COUNT,HT_CLASS6_COUNT,HT_CLASS7_COUNT,HT_CLASS8_COUNT,
Created,LastModified,IsDeleted
    FROM OPENJSON (@fromjson) 
    WITH (
PLOTID nvarchar(50),
SMALLTREEID nvarchar(50),
PLOTID nvarchar(max),
SPECIES int,
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
IsDeleted nvarchar(max)
    ))
    update u SET 
u.SMALLTREEID=jd.SMALLTREEID,
u.PLOTID=jd.PLOTID,
u.SPECIES=jd.SPECIES,
u.HT_CLASS1_COUNT=jd.HT_CLASS1_COUNT,
u.HT_CLASS2_COUNT=jd.HT_CLASS2_COUNT,
u.HT_CLASS3_COUNT=jd.HT_CLASS3_COUNT,
u.HT_CLASS4_COUNT=jd.HT_CLASS4_COUNT,
u.HT_CLASS5_COUNT=jd.HT_CLASS5_COUNT,
u.HT_CLASS6_COUNT=jd.HT_CLASS6_COUNT,
u.HT_CLASS7_COUNT=jd.HT_CLASS7_COUNT,
u.HT_CLASS8_COUNT=jd.HT_CLASS8_COUNT,
u.Created=jd.Created,
u.LastModified=jd.LastModified,
u.IsDeleted=jd.IsDeleted,
	u.LastModifiedAtServer = CURRENT_TIMESTAMP
	from SMALLTREE as u
	inner join json_data as jd on jd.SMALLTREEID = u.SMALLTREEID
   
    RETURN 1
  END TRY
  BEGIN CATCH
     RETURN 0
  END CATCH

END
GO
/****** Object:  StoredProcedure [dbo].[UpdateSoil]    Script Date: 4/14/2020 9:40:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/****** Object:  StoredProcedure [dbo].[InsertProject]    Script Date: 4/13/2020 2:58:46 PM ******/

CREATE PROCEDURE [dbo].[UpdateSoil](@fromjson nvarchar(max))
AS BEGIN

  BEGIN TRY
 
    with json_data as (select SOILID,PLOTID,LAYER,[FROM],[TO],HORIZON,VON_POST,TEXTURE,PORE_PATTERN,STRUCTURE,COLOUR,MOTTLE_COLOUR,PERCENT_GRAVEL,PERCENT_COBBLE,PERCENT_STONE,Created,LastModified,IsDeleted
    FROM OPENJSON (@fromjson) 
    WITH (
SOILID nvarchar(max),
PLOTID nvarchar(max),
LAYER int,
[FROM] int,
[TO] int,
HORIZON nvarchar(max),
VON_POST nvarchar(max),
TEXTURE nvarchar(max),
PORE_PATTERN nvarchar(max),
STRUCTURE nvarchar(max),
COLOUR nvarchar(max),
MOTTLE_COLOUR nvarchar(max),
PERCENT_GRAVEL int,
PERCENT_COBBLE int,
PERCENT_STONE int,
Created datetime2,
LastModified datetime2,
IsDeleted nvarchar(max)
    ))
    update u SET 
u.SOILID=jd.SOILID,
u.PLOTID=jd.PLOTID,
u.LAYER=jd.LAYER,
u.[FROM]=jd.[FROM],
u.[TO]=jd.[TO],
u.HORIZON=jd.HORIZON,
u.VON_POST=jd.VON_POST,
u.TEXTURE=jd.TEXTURE,
u.PORE_PATTERN=jd.PORE_PATTERN,
u.STRUCTURE=jd.STRUCTURE,
u.COLOUR=jd.COLOUR,
u.MOTTLE_COLOUR=jd.MOTTLE_COLOUR,
u.PERCENT_GRAVEL=jd.PERCENT_GRAVEL,
u.PERCENT_COBBLE=jd.PERCENT_COBBLE,
u.PERCENT_STONE=jd.PERCENT_STONE,
u.Created=jd.Created,
u.LastModified=jd.LastModified,
u.IsDeleted=jd.IsDeleted,
u.LastModifiedAtServer = CURRENT_TIMESTAMP
	from SOIL as u
	inner join json_data as jd on jd.SOILID = u.SOILID
  
    RETURN 1
  END TRY
  BEGIN CATCH
     RETURN 0
  END CATCH

END
GO
/****** Object:  StoredProcedure [dbo].[UpdateStemmap]    Script Date: 4/14/2020 9:40:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/****** Object:  StoredProcedure [dbo].[InsertProject]    Script Date: 4/13/2020 2:58:46 PM ******/

CREATE PROCEDURE [dbo].[UpdateStemmap](@fromjson nvarchar(max))
AS BEGIN

  BEGIN TRY
 
    with json_data as (select STEMMAPID,TREEID,AZIMUTH,DISTANCE,CROWN_AXIS_LONG,CROWN_AXIS_SHORT,OFFSET_AZIMUTH,OFFSET_DISTANCE,Created,LastModified,IsDeleted
    FROM OPENJSON (@fromjson) 
    WITH (
STEMMAPID nvarchar(50),
TREEID nvarchar(max),
AZIMUTH int,
DISTANCE float,
CROWN_AXIS_LONG float,
CROWN_AXIS_SHORT float,
OFFSET_AZIMUTH int,
OFFSET_DISTANCE float,
Created datetime2,
LastModified datetime2,
IsDeleted nvarchar(max)
    ))
    update u SET 
u.STEMMAPID=jd.STEMMAPID,
u.TREEID=jd.TREEID,
u.AZIMUTH=jd.AZIMUTH,
u.DISTANCE=jd.DISTANCE,
u.CROWN_AXIS_LONG=jd.CROWN_AXIS_LONG,
u.CROWN_AXIS_SHORT=jd.CROWN_AXIS_SHORT,
u.OFFSET_AZIMUTH=jd.OFFSET_AZIMUTH,
u.OFFSET_DISTANCE=jd.OFFSET_DISTANCE,
u.Created=jd.Created,
u.LastModified=jd.LastModified,
u.IsDeleted=jd.IsDeleted,
u.LastModifiedAtServer = CURRENT_TIMESTAMP
	from STEMMAP as u
	inner join json_data as jd on jd.STEMMAPID = u.STEMMAPID
  
    RETURN 1
  END TRY
  BEGIN CATCH
     RETURN 0
  END CATCH

END
GO
/****** Object:  StoredProcedure [dbo].[UpdateTree]    Script Date: 4/14/2020 9:40:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/****** Object:  StoredProcedure [dbo].[InsertProject]    Script Date: 4/13/2020 2:58:46 PM ******/

CREATE PROCEDURE [dbo].[UpdateTree](@fromjson nvarchar(max))
AS BEGIN

  BEGIN TRY
 
    with json_data as (select TREEID,PLOTID,SECTION,TREENUM,SPECIES,TAG_TYPE,ORIGIN,STATUS,VIGOUR,HT_TO_DBH,DBH,HT,LENGTH,DBH_IN,CROWN_IN,LIVE_CROWN_RATIO,CROWN_CLASS,CROWN_POSITION,CROWN_DAMAGE,DEFOLIATING_INSECT,
FOLIAR_DISEASE,STEM_QUALITY,BARK_RETENTION,WOOD_CONDITION,DECAY_CLASS,MORT_CAUSE,BROKEN_TOP,AGE,AGE_OFFICE,AGE_CORE_LENGTH,AZIMUTH,DISTANCE,CROWN_WIDTH1,CROWN_WIDTH2,COMMENTS,
Created,LastModified,IsDeleted
    FROM OPENJSON (@fromjson) 
    WITH (
TREEID nvarchar(50),
PLOTID nvarchar(max),
SECTION int,
TREENUM int,
SPECIES int,
TAG_TYPE nvarchar(max),
ORIGIN nvarchar(max),
STATUS nvarchar(max),
VIGOUR int,
HT_TO_DBH float,
DBH float,
HT float,
LENGTH float,
DBH_IN nvarchar(max),
CROWN_IN nvarchar(max),
LIVE_CROWN_RATIO int,
CROWN_CLASS nvarchar(max),
CROWN_POSITION int,
CROWN_DAMAGE int,
DEFOLIATING_INSECT int,
FOLIAR_DISEASE int,
STEM_QUALITY nvarchar(max),
BARK_RETENTION int,
WOOD_CONDITION int,
DECAY_CLASS int,
MORT_CAUSE int,
BROKEN_TOP nvarchar(max),
AGE int,
AGE_OFFICE int,
AGE_CORE_LENGTH float,
AZIMUTH float,
DISTANCE float,
CROWN_WIDTH1 float,
CROWN_WIDTH2 float,
COMMENTS nvarchar(max),
Created datetime2,
LastModified datetime2,
IsDeleted nvarchar(max)
    ))
    update u SET 
u.TREEID=jd.TREEID,
u.PLOTID=jd.PLOTID,
u.SECTION=jd.SECTION,
u.TREENUM=jd.TREENUM,
u.SPECIES=jd.SPECIES,
u.TAG_TYPE=jd.TAG_TYPE,
u.ORIGIN=jd.ORIGIN,
u.STATUS=jd.STATUS,
u.VIGOUR=jd.VIGOUR,
u.HT_TO_DBH=jd.HT_TO_DBH,
u.DBH=jd.DBH,
u.HT=jd.HT,
u.LENGTH=jd.LENGTH,
u.DBH_IN=jd.DBH_IN,
u.CROWN_IN=jd.CROWN_IN,
u.LIVE_CROWN_RATIO=jd.LIVE_CROWN_RATIO,
u.CROWN_CLASS=jd.CROWN_CLASS,
u.CROWN_POSITION=jd.CROWN_POSITION,
u.CROWN_DAMAGE=jd.CROWN_DAMAGE,
u.DEFOLIATING_INSECT=jd.DEFOLIATING_INSECT,
u.FOLIAR_DISEASE=jd.FOLIAR_DISEASE,
u.STEM_QUALITY=jd.STEM_QUALITY,
u.BARK_RETENTION=jd.BARK_RETENTION,
u.WOOD_CONDITION=jd.WOOD_CONDITION,
u.DECAY_CLASS=jd.DECAY_CLASS,
u.MORT_CAUSE=jd.MORT_CAUSE,
u.BROKEN_TOP=jd.BROKEN_TOP,
u.AGE=jd.AGE,
u.AGE_OFFICE=jd.AGE_OFFICE,
u.AGE_CORE_LENGTH=jd.AGE_CORE_LENGTH,
u.AZIMUTH=jd.AZIMUTH,
u.DISTANCE=jd.DISTANCE,
u.CROWN_WIDTH1=jd.CROWN_WIDTH1,
u.CROWN_WIDTH2=jd.CROWN_WIDTH2,
u.COMMENTS=jd.COMMENTS,
u.Created=jd.Created,
u.LastModified=jd.LastModified,
u.IsDeleted=jd.IsDeleted,
u.LastModifiedAtServer = CURRENT_TIMESTAMP
	from TREE as u
	inner join json_data as jd on jd.TREEID = u.TREEID
  
    RETURN 1
  END TRY
  BEGIN CATCH
     RETURN 0
  END CATCH

END
GO
/****** Object:  StoredProcedure [dbo].[UpdateVegetation]    Script Date: 4/14/2020 9:40:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/****** Object:  StoredProcedure [dbo].[InsertProject]    Script Date: 4/13/2020 2:58:46 PM ******/

CREATE PROCEDURE [dbo].[UpdateVegetation](@fromjson nvarchar(max))
AS BEGIN

  BEGIN TRY
 
    with json_data as (select VEGETATIONID,PLOTID,SPECIES,QUAD1,QUAD2,QUAD3,QUAD4,ELCLAYER3,ELCLAYER4,ELCLAYER5,ELCLAYER6,ELCLAYER7,Created,LastModified,IsDeleted
    FROM OPENJSON (@fromjson) 
    WITH (
VEGETATIONID nvarchar(50),
PLOTID nvarchar(max),
SPECIES nvarchar(max),
QUAD1 int,
QUAD2 int,
QUAD3 int,
QUAD4 int,
ELCLAYER3 int,
ELCLAYER4 int,
ELCLAYER5 int,
ELCLAYER6 int,
ELCLAYER7 int,
Created datetime2,
LastModified datetime2,
IsDeleted nvarchar(max)
    ))
    update u SET 
u.VEGETATIONID=jd.VEGETATIONID,
u.PLOTID=jd.PLOTID,
u.SPECIES=jd.SPECIES,
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
u.LastModifiedAtServer = CURRENT_TIMESTAMP
	from VEGETATION as u
	inner join json_data as jd on jd.VEGETATIONID = u.VEGETATIONID
  
    RETURN 1
  END TRY
  BEGIN CATCH
     RETURN 0
  END CATCH

END
GO
ALTER DATABASE [eLiDAR] SET  READ_WRITE 
GO
