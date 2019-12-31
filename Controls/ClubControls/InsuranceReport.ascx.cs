﻿using MyFlightbook;
using MyFlightbook.Clubs;
using MyFlightbook.FlightCurrency;
using System;
using System.Web.UI.WebControls;

/******************************************************
 * 
 * Copyright (c) 2019 MyFlightbook LLC
 * Contact myflightbook-at-gmail.com for more information
 *
*******************************************************/
public partial class Controls_ClubControls_InsuranceReport : System.Web.UI.UserControl
{
    public string CSVData
    {
        get { return gvInsuranceReport.CSVFromData(); }
    }

    protected string CSSForItem(CurrencyState cs)
    {
        switch (cs)
        {
            case CurrencyState.GettingClose:
                return "currencynearlydue";
            case CurrencyState.NotCurrent:
                return "currencyexpired";
            case CurrencyState.OK:
                return "currencyok";
            case CurrencyState.NoDate:
                return "currencynodate";
        }
        return string.Empty;
    }

    public void Refresh(int ClubID, int monthInterval)
    {
        gvInsuranceReport.DataSource = ClubInsuranceReportItem.ReportForClub(ClubID, monthInterval);
        gvInsuranceReport.DataBind();
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void gvInsuranceReport_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e == null)
            throw new ArgumentNullException("e");
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ClubInsuranceReportItem ciri = (ClubInsuranceReportItem)e.Row.DataItem;
            GridView gvTimeInAircraft = (GridView)e.Row.FindControl("gvAircraftTime");
            gvTimeInAircraft.DataSource = ciri.TotalsByClubAircraft;
            gvTimeInAircraft.DataBind();
            Repeater rptPilotStatus = (Repeater)e.Row.FindControl("rptPilotStatus");
            rptPilotStatus.DataSource = ciri.PilotStatusItems;
            rptPilotStatus.DataBind();
        }
    }
}