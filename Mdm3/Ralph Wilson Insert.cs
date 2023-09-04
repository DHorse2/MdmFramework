CREATE proc [dbo].[Transert] 
    @iStCode integer,
    @iStAcct as decimal(11,2),
    @iTranType as varchar(3),
    @iTranDt as varchar(10),
    @iPrincipal as decimal(11,2),
    @iInterest as decimal(11,2),
    @iReceiptType varchar(1),
    @iCreateWho varchar(10),
    @iRnbr varchar(10)
AS
    DECLARE 
        @oInt decimal(11,2),
        @iReceiptNo int
    Begin Try
        set @iReceiptNo = 
        (
            select max(receiptno)+1
            from dbo.streettrans
            where 
                stcode > 55
                and trantype = 'PMT'
        );
        insert into streetassessments.dbo.streettrans
        (
            StCode,
            StAcct,
            TranType,
            TranDt,
            Principal,
            Interest,
            ReceiptType,
            ReceiptNo,
            CreateWho,
            CreateDt,
            Rnbr
        )
        values
        (
            @iStCode,
            @iStAcct,
            @iTranType,
            convert(datetime,@iTranDt,110),
            @iPrincipal,
            @iInterest,
            @iReceiptType,
            @iReceiptNo,
            @iCreateWho,
            getdate(),
            @iRnbr
        );
        select 
            t.stcode,
            t.rnbr,
            t.trantype,
            t.receiptno,
            convert(varchar(25),t.createdt,120),
            t.createwho,
            t.principal,
            t.interest,
            convert(varchar(10),t.trandt,110),
            a.name1,
            a.name2,
            a.name3,
            a.addr,
            a.city,
            a.state,
            a.zip,
            a.zip4,
            streetassessments.dbo.getCurBal(@iStCode,@iRnbr),
            streetassessments.dbo.getLppDtc(@iStCode,@iRnbr),
            streetassessments.dbo.months_between(dbo.getLppDtc( @iStCode,@iRnbr),convert(varchar(10),getdate(),110 ),@iStCode),
            streetassessments.dbo.intCalc(@iStCode,@iRnbr,conve rt(varchar(10),getdate(),110)),
            convert(varchar(10),getdate(),110)
        from streettrans t, streetaccts a
        where 
            a.stcode = t.stcode
            and a.rnbr = t.rnbr
            and t.stcode = @iStCode
            and t.rnbr = @iRnbr
            and t.receiptno = @iReceiptNo;
    End Try
    Begin catch
        insert into StreetAssessments.dbo.ErrLog
        (
            Tstamp,
            ErrorNumber,
            ErrorMessage,
            ErrorSeverity,
            ErrorState,
            ErrorLine,
            ErrorProcedure
        )
        select getdate(),
        Error_Number(),
        Error_Message(),
        Error_Severity(),
        Error_State(),
        Error_Line(),
        Error_Procedure()
    End catch ...