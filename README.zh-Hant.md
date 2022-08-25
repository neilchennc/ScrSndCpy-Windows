# ScrSndCpy (Windows)

*ScrSndCpy = **Scr**een **S**ou**nd** **C**o**py***

[English](README.md)

Windows圖形化介面的程式，可以同時執行[scrcpy](https://github.com/Genymobile/scrcpy)和[sndcpy](https://github.com/rom1v/sndcpy)，用來顯示畫面、聲音與控制Android手機或平板，

![Screenshot](screenshots/scrsndcpy-screen.png "Screenshot")

*想找Linux版本？[點我](https://github.com/neilchennc/ScrSndCpy-Linux)*

## 關於scrcpy與sndcpy

[scrcpy](https://github.com/Genymobile/scrcpy) (Screen Copy)是由Genymobile開發的程式，可以透過USB或是網路（TCP/IP）來顯示或控制Android裝置（至少要Android 5.0以上）。

[sndcpy](https://github.com/rom1v/sndcpy) (Sound Copy)是由rom1v開發的程式，可以透過USB或是網路（TCP/IP）將Android裝置的聲音傳到電腦播放（至少要Android 10以上，電腦需安裝[VLC](https://www.videolan.org/)才能播放聲音）。

兩個皆不需要root權限。

## 需求

- Windows 7/8/10/11

- Android 5.0以上（如果想要聲音輸出，則要Android 10以上）

- 安裝[VLC](https://www.videolan.org/)（安裝在電腦，用來播放聲音）

## 下載

- [ScrSndCpy-v1.0.zip](https://github.com/neilchennc/ScrSndCpy-Windows/releases/download/v1.0/ScrSndCpy-v1.0.zip)

  SHA-256: aed5f62a458cff8e34606144bca0ac8b4cd43ae1eae0ba5fecfb868f959dac2d

## 如何使用

在使用ScrSndCpy之前，你的Android手機必須先[啟用偵錯模式](https://developer.android.com/studio/command-line/adb.html#Enabling)，或者可以去找[YouTube教學影片](https://www.youtube.com/results?search_query=android+usb+%E5%81%B5%E9%8C%AF%E6%A8%A1%E5%BC%8F)

### 透過USB連接

- 下載最新的ScrSndCpy並解壓縮

- 執行**ScrSndCpy.exe**

- 將Android裝置插入USB線，連接電腦

- 此時你的Android裝置會跳出一個USB偵錯的確認對話框，按下允許按鈕

- 在ScrSndCpy裡選擇一個裝置

- 點擊**Play**按鈕

### 透過無線方式連接

- 下載最新的ScrSndCpy並解壓縮

- 執行**ScrSndCpy.exe**

- Android裝置連接到Wi-Fi，注意你的電腦也必須連到同樣的網路（網段）

- 在ScrSndCpy輸入Android裝置的IP位址（預設連接埠為5555）

- 點擊**Play**按鈕

- 此時你的Android裝置會跳出一個USB偵錯的確認對話框，按下允許按鈕

- 再次點擊**Play**按鈕

## 快捷鍵（參考自scrcpy 1.24）

In the following list, <kbd>MOD</kbd> is the shortcut modifier. By default, it's
(left) <kbd>Alt</kbd> or (left) <kbd>Super</kbd>.

_<kbd>[Super]</kbd> is typically the <kbd>Windows</kbd> or <kbd>Cmd</kbd> key._

[Super]: https://en.wikipedia.org/wiki/Super_key_(keyboard_button)

 | Action                                      |   Shortcut
 | ------------------------------------------- |:-----------------------------
 | Switch fullscreen mode                      | <kbd>MOD</kbd>+<kbd>f</kbd>
 | Rotate display left                         | <kbd>MOD</kbd>+<kbd>←</kbd> _(left)_
 | Rotate display right                        | <kbd>MOD</kbd>+<kbd>→</kbd> _(right)_
 | Resize window to 1:1 (pixel-perfect)        | <kbd>MOD</kbd>+<kbd>g</kbd>
 | Resize window to remove black borders       | <kbd>MOD</kbd>+<kbd>w</kbd> \| _Double-left-click¹_
 | Click on `HOME`                             | <kbd>MOD</kbd>+<kbd>h</kbd> \| _Middle-click_
 | Click on `BACK`                             | <kbd>MOD</kbd>+<kbd>b</kbd> \| _Right-click²_
 | Click on `APP_SWITCH`                       | <kbd>MOD</kbd>+<kbd>s</kbd> \| _4th-click³_
 | Click on `MENU` (unlock screen)⁴            | <kbd>MOD</kbd>+<kbd>m</kbd>
 | Click on `VOLUME_UP`                        | <kbd>MOD</kbd>+<kbd>↑</kbd> _(up)_
 | Click on `VOLUME_DOWN`                      | <kbd>MOD</kbd>+<kbd>↓</kbd> _(down)_
 | Click on `POWER`                            | <kbd>MOD</kbd>+<kbd>p</kbd>
 | Power on                                    | _Right-click²_
 | Turn device screen off (keep mirroring)     | <kbd>MOD</kbd>+<kbd>o</kbd>
 | Turn device screen on                       | <kbd>MOD</kbd>+<kbd>Shift</kbd>+<kbd>o</kbd>
 | Rotate device screen                        | <kbd>MOD</kbd>+<kbd>r</kbd>
 | Expand notification panel                   | <kbd>MOD</kbd>+<kbd>n</kbd> \| _5th-click³_
 | Expand settings panel                       | <kbd>MOD</kbd>+<kbd>n</kbd>+<kbd>n</kbd> \| _Double-5th-click³_
 | Collapse panels                             | <kbd>MOD</kbd>+<kbd>Shift</kbd>+<kbd>n</kbd>
 | Copy to clipboard⁵                          | <kbd>MOD</kbd>+<kbd>c</kbd>
 | Cut to clipboard⁵                           | <kbd>MOD</kbd>+<kbd>x</kbd>
 | Synchronize clipboards and paste⁵           | <kbd>MOD</kbd>+<kbd>v</kbd>
 | Inject computer clipboard text              | <kbd>MOD</kbd>+<kbd>Shift</kbd>+<kbd>v</kbd>
 | Enable/disable FPS counter (on stdout)      | <kbd>MOD</kbd>+<kbd>i</kbd>
 | Pinch-to-zoom                               | <kbd>Ctrl</kbd>+_click-and-move_
 | Drag & drop APK file                        | Install APK from computer
 | Drag & drop non-APK file                    | Push file to device

_¹Double-click on black borders to remove them._  
_²Right-click turns the screen on if it was off, presses BACK otherwise._  
_³4th and 5th mouse buttons, if your mouse has them._  
_⁴For react-native apps in development, `MENU` triggers development menu._  
_⁵Only on Android >= 7._

你可以到[scrcpy](https://github.com/Genymobile/scrcpy)或[sndcpy](https://github.com/rom1v/sndcpy)查看更多它們的詳細介紹、操作、功能等
