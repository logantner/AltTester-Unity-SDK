using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;
using System;

public class AltConsole : MonoBehaviour {

    // inspector attributes
    public int tapsToShow;
    public GameObject canvas;
    public ScrollRect scrollView;
    public Text logText;
    public Toggle toggleLogInfo;
    public Toggle toggleLogWarn;
    public Toggle toggleLogError;
    public Toggle toggleLogException;

    private int logLineCounter = 0;
    private bool doAutoScroll = true;

	private string messageToLog; 
	private bool updateLogs; 
	
    void Awake() {
        logText.text = "";
		messageToLog = "";
		updateLogs = false;
    }

    void Start() {
        if (PlayerPrefs.HasKey("DEBUG_LOG_INFO")) {
            toggleLogInfo.isOn = PlayerPrefs.GetInt("DEBUG_LOG_INFO") == 1;
        }
        if (PlayerPrefs.HasKey("DEBUG_LOG_WARN")) {
            toggleLogWarn.isOn = PlayerPrefs.GetInt("DEBUG_LOG_WARN") == 1;
        }
        if (PlayerPrefs.HasKey("DEBUG_LOG_ERROR")) {
            toggleLogError.isOn = PlayerPrefs.GetInt("DEBUG_LOG_ERROR") == 1;
        }
        if (PlayerPrefs.HasKey("DEBUG_LOG_EXCEPTION")) {
            toggleLogException.isOn = PlayerPrefs.GetInt("DEBUG_LOG_EXCEPTION") == 1;
        }
    }

    public void DebugOptionsChanged() {
        PlayerPrefs.SetInt("DEBUG_LOG_INFO", Convert.ToInt32(toggleLogInfo.isOn));
        PlayerPrefs.SetInt("DEBUG_LOG_WARN", Convert.ToInt32(toggleLogWarn.isOn));
        PlayerPrefs.SetInt("DEBUG_LOG_ERROR", Convert.ToInt32(toggleLogError.isOn));
        PlayerPrefs.SetInt("DEBUG_LOG_EXCEPTION", Convert.ToInt32(toggleLogException.isOn));
        PlayerPrefs.Save();

    }
	
    void OnEnable() {
        Application.logMessageReceivedThreaded += HandleLog;

    }

    void OnDisable() {
        Application.logMessageReceivedThreaded -= HandleLog;
    }

    void Update() {
       if (updateLogs) {
		   	logText.text += messageToLog;
        	logLineCounter++;
            scrollView.verticalNormalizedPosition = 0;
			updateLogs = false;
	   }
    }

    void HandleLog(string message, string stackTrace, LogType type) {
        bool writeLog = false;
        switch (type) {
            case LogType.Log:
                if (toggleLogInfo.isOn)
                    writeLog = true;
                break;
            case LogType.Warning:
                if (toggleLogWarn.isOn)
                    writeLog = true;
                break;
            case LogType.Error:
                if (toggleLogError.isOn)
                    writeLog = true;
                break;
            case LogType.Exception:
                if (toggleLogException.isOn)
                    writeLog = true;
                break;
        }
        if (writeLog) {
            messageToLog = message + "\n";
			updateLogs = true;
        }
    }

    public void ScrollToTop() {
        scrollView.verticalNormalizedPosition = 1.0F;
        doAutoScroll = false;
    }

    public void ScrollToBottom() {
        scrollView.verticalNormalizedPosition = 0;
        doAutoScroll = true;
    }

    public void Hide() {
        canvas.SetActive(false);
    }

	public void Show() {
        canvas.SetActive(true);
    }

    public void Clear() {
        logText.text = "";
        logLineCounter = 0;
    }
}