USE [master]
GO
/****** Object:  Database [betaTekvah]    Script Date: 19/07/2018 09:08:54 ******/
CREATE DATABASE [betaTekvah]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'betaTekvah', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\betaTekvah.mdf' , SIZE = 4288KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'betaTekvah_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\betaTekvah_log.ldf' , SIZE = 1072KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [betaTekvah] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [betaTekvah].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [betaTekvah] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [betaTekvah] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [betaTekvah] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [betaTekvah] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [betaTekvah] SET ARITHABORT OFF 
GO
ALTER DATABASE [betaTekvah] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [betaTekvah] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [betaTekvah] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [betaTekvah] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [betaTekvah] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [betaTekvah] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [betaTekvah] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [betaTekvah] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [betaTekvah] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [betaTekvah] SET  ENABLE_BROKER 
GO
ALTER DATABASE [betaTekvah] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [betaTekvah] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [betaTekvah] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [betaTekvah] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [betaTekvah] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [betaTekvah] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [betaTekvah] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [betaTekvah] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [betaTekvah] SET  MULTI_USER 
GO
ALTER DATABASE [betaTekvah] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [betaTekvah] SET DB_CHAINING OFF 
GO
ALTER DATABASE [betaTekvah] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [betaTekvah] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [betaTekvah] SET DELAYED_DURABILITY = DISABLED 
GO
USE [betaTekvah]
GO
/****** Object:  Table [dbo].[transaction]    Script Date: 19/07/2018 09:08:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[transaction](
	[invoiceNo] [int] IDENTITY(1,1) NOT NULL,
	[date] [datetime] NULL,
	[type] [nchar](10) NULL,
	[total] [decimal](18, 2) NULL,
 CONSTRAINT [PK_transaction] PRIMARY KEY CLUSTERED 
(
	[invoiceNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  StoredProcedure [dbo].[proc_GroupTransactionByDate]    Script Date: 19/07/2018 09:08:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[proc_GroupTransactionByDate](@FromDate datetime=NULL,
											@ToDate datetime=NULL,
											@IsForCsv bit=1)as
begin

 SET NOCOUNT on

if (@FromDate is null and @ToDate is null)
begin
set @FromDate= cast(getdate() as date);
set @ToDate=cast(cast (getdate() as date) as datetime) + cast( '23:59:59.000' as datetime);
end


DECLARE @retTable TABLE (TranDate Date,Total_Count int,Total_Amount decimal(18,2),Total_Visa int,Total_Visa_Amount decimal(18,2),Total_Cash int,Total_Cash_Amount decimal(18,2))
declare @v_diff_between_days int =DATEDIFF(DAY,@FromDate,@ToDate);
declare @v_current_date datetime=@FromDate;
declare @v_Total_Count int=0;
declare @v_Total_Visa int=0;
declare @v_Total_Cash int=0;
declare @v_total_Amount decimal(18,2)=0;
declare @v_total_Visa_Amount decimal(18,2)=0;
declare @v_total_Cash_Amount decimal(18,2)=0;

while @v_diff_between_days>=0
begin
     
set @v_Total_Count =0;
set @v_Total_Visa =0;
set @v_Total_Cash =0;
set @v_total_Amount =0;
set @v_total_Visa_Amount =0;
set @v_total_Cash_Amount =0;

select @v_Total_Count=count(*),@v_total_Amount=sum([total])
from [transaction]
where cast([date] as date)=cast(@v_current_date as date)
group by cast([date] as date)


select @v_Total_Visa=count(*),@v_total_Visa_Amount=sum([total])
from [transaction]
where cast([date] as date)=cast(@v_current_date as date) and [type] like N'מזומן     '
group by cast([date] as date)


select @v_Total_Cash=count(*),@v_total_Cash_Amount=sum([total])
from [transaction]
where cast([date] as date)=cast(@v_current_date as date) and [type] like N'אשראי     '
group by cast([date] as date)


insert into @retTable
values(@v_current_date,@v_Total_Count,@v_total_Amount,@v_Total_Visa,@v_total_Visa_Amount,@v_Total_Cash,@v_total_Cash_Amount)


set @v_current_date=DateAdd(day,1,@v_current_date);
set @v_diff_between_days=@v_diff_between_days-1;

end

if(@IsForCsv=1)
	begin
	        select N'תאריך',N'עסקאות',N'סכום',N'עסקאות אשראי',N'סכום',N'עסקאות מזומן',N'סכום'
			

	        SELECT * 
			FROM @retTable
	end
else
	begin
			SELECT * 
			FROM @retTable
	end





end
GO
/****** Object:  StoredProcedure [dbo].[proc_SelectTransaction]    Script Date: 19/07/2018 09:08:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[proc_SelectTransaction](@FromDate datetime=NULL,
												@ToDate datetime=NULL,
												@IsForCsv bit=1)as
begin
 SET NOCOUNT on

if (@FromDate is null and @ToDate is null)
begin
set @FromDate= cast(getdate() as date);
set @ToDate=cast(cast (getdate() as date) as datetime) + cast( '23:59:59.000' as datetime);
end


if(@IsForCsv=1)
	begin
	      select N'מספר עסקה',N'תאריך',N'שעה',N'סוג העסקה',N'סכום'	
	end

select [invoiceNo],cast([date] as date)as date,cast([date] as time) as time,[type],[total]
from [transaction]
where [date] between @FromDate and @ToDate


end
GO
USE [master]
GO
ALTER DATABASE [betaTekvah] SET  READ_WRITE 
GO
