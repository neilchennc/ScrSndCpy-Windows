# ScrSndCpy

*ScrSndCpy = **Scr**een **S**ou**nd** **C**o**py***

Windows圖形化介面的程式，可以同時執行[scrcpy](https://github.com/Genymobile/scrcpy)和[sndcpy](https://github.com/rom1v/sndcpy)，用來顯示畫面、聲音與控制Android手機或平板，

![Screenshot](screenshots/scrsndcpy-screen.png "Screenshot")

## 關於scrcpy與sndcpy

[scrcpy](https://github.com/Genymobile/scrcpy) (Screen Copy)是由Genymobile開發的程式，可以透過USB或是網路（TCP/IP）來顯示或控制Android裝置（至少要Android 5.1以上）。

[sndcpy](https://github.com/rom1v/sndcpy) (Sound Copy)是由rom1v開發的程式，可以透過USB或是網路（TCP/IP）將Android裝置的聲音傳到電腦播放（至少要Android 10以上，電腦需安裝[VLC](https://www.videolan.org/)才能播放聲音）。

兩個皆不需要root權限。

## 下載

TODO

## 如何使用

使用ScrSndCpy之前，你的Android手機必須先[啟用偵錯模式](https://developer.android.com/studio/command-line/adb.html#Enabling)

- 將Android裝置插入USB（**有線**的方式），或者連接Wi-Fi並查看IP位址（**無線**的方式）

- 下載最新的ScrSndCpy並解壓縮

- 執行**ScrSndCpy.exe**

- 選擇一個裝置，或是輸入IP位址（預設連接埠為5555）

- 點擊**Play**按鈕

你可以到[scrcpy](https://github.com/Genymobile/scrcpy)或[sndcpy](https://github.com/rom1v/sndcpy)查看更多的詳細說明
