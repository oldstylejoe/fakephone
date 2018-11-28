//Joe Snider
//2/18
//
//Adapted from earlier work. Enable saving to google drive.
//Have to setup the auth pretty generically, so it could bog down.
//
//be sure to add this to exactly one object (like the main camera) that will persist for the whole game
//  (make a blank object if you have to).

using UnityEngine;
using System.Runtime.InteropServices;
using System.IO;
using System;
using UnityGoogleDrive;
using System.Text;
using System.Collections.Generic;
using UnityGoogleDrive.Data;
using System.Diagnostics;

public class Clock : Singleton<Clock>
{

    public string subjName = "default";
    private int uploadID = 0;
    private string data = "";

    private Stopwatch stopWatch;

    //private void OnApplicationQuit()
    //{
    //upload();
    //}

    private void Start()
    {
        stopWatch.Start();
    }

    public void write(string str)
    {
        data += str;
    }

    public void clearData()
    {
        data = "";
    }

    //do the upload to google drive
    public void upload()
    {
        if (data.Length == 0) { return; }

        var file = new UnityGoogleDrive.Data.File() {
            Name = "log_" + uploadID.ToString() + ".txt",
            Parents = new List<string>{ "1UANcfS3aWNjJwmsb1dA--FUDX_Guj2rF" },   //<<<<<------hacked in for now
            Content =  Encoding.ASCII.GetBytes(data)};
        var result = GoogleDriveFiles.Create(file).Send();
        //TODO: add some flag to the OnDone event to manage conflicts (or call infrequently).
        uploadID += 1;
        clearData();
    }

    //automate the time stamping. Slight loss of precision is possible (but unlikely).
    public void markEvent(string str)
    {
        long fTest = stopWatch.ElapsedTicks;
        System.DateTime dt = DateTime.Now;
        dt = dt.ToLocalTime();
        write(str + " " + "default" + " " +
            dt.ToString("yyyy-MM-dd HH:mm:ss.ffffff") + " " + fTest.ToString() + "\n");
    }


}