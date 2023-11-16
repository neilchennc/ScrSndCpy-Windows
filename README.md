# ScrSndCpy (Windows)

*ScrSndCpy = **Scr**een **S**ou**nd** **C**o**py***

[繁體中文](README.zh-Hant.md)

A Windows GUI application to dispaly screen, audio and control your Android devices on PC by executing [scrcpy](https://github.com/Genymobile/scrcpy) (passing commands and parameters to [scrcpy](https://github.com/Genymobile/scrcpy)).

![Screenshot](screenshots/scrsndcpy-screen.png "Screenshot")

*Looking for Linux version? [Click here](https://github.com/neilchennc/ScrSndCpy-Linux)*

## About scrcpy

[scrcpy](https://github.com/Genymobile/scrcpy) (Screen Copy) is an application provides display and control of Android devices connected via USB or over TCP/IP (requires at least Android 5.0) which developed by Genymobile.

It does NOT require any root access.

## Requirements

- Windows 7/8/10/11

- Android 5.0+ (Audio forwarding must Android 11+)

## Download

- [ScrSndCpy-v1.3.zip](https://github.com/neilchennc/ScrSndCpy-Windows/releases/download/v1.3/ScrSndCpy-v1.3.zip)

  SHA-256: f2cb4a593155fea9b38a4199a59b8c2b43bf224de8410d417ab33a0a2d6df5c9

## How to use

### Preparation

- Your Android devices must [enable debugging mode](https://developer.android.com/studio/command-line/adb.html#Enabling) before using ScrSndCpy, or find [tutorial videos on YouTube](https://www.youtube.com/results?search_query=android+enable+usb+debugging).

- Download latest ScrSndCpy and extract it

- Run **ScrSndCpy.exe**

### Method 1: Connect via USB

- Plug the device into a USB port on your computer

- Your device will popup a USB debugging confirmation dialog, click "Allow" button

- In ScrSndCpy, select a device from the list

- Click **Play** button

### Method 2: Connect via Wi-Fi (TCP/IP)

#### Setup (**only for the first time**)

You have to enable tcp port on your device with following steps

- Plug the device into a USB port on your computer

- Enable adb over TCP/IP on your device with command:

  ```
  adb.exe tcpip 5555
  ```

- Unplug your device

#### Connect

- Connect to Wi-Fi access point, note that your PC must connect to the same access point (network segment)

- In the ScrSndCpy, enter Android device's IP address

- Click **Play** button

- Your device will popup a USB debugging confirmation dialog, click "Allow" button

- Click **Play** button again

## Shortcuts (references from [scrcpy 2.2 shortcut](https://github.com/Genymobile/scrcpy/blob/master/doc/shortcuts.md))

Actions can be performed on the scrcpy window using keyboard and mouse
shortcuts.

In the following list, <kbd>MOD</kbd> is the shortcut modifier. By default, it's
(left) <kbd>Alt</kbd> or (left) <kbd>Super</kbd>.

It can be changed using `--shortcut-mod`. Possible keys are `lctrl`, `rctrl`,
`lalt`, `ralt`, `lsuper` and `rsuper`. For example:

```bash
# use RCtrl for shortcuts
scrcpy --shortcut-mod=rctrl

# use either LCtrl+LAlt or LSuper for shortcuts
scrcpy --shortcut-mod=lctrl+lalt,lsuper
```

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
 | Click on `BACK`                             | <kbd>MOD</kbd>+<kbd>b</kbd> \| <kbd>MOD</kbd>+<kbd>Backspace</kbd> \| _Right-click²_
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
 | Drag & drop non-APK file                    | [Push file to device](https://github.com/Genymobile/scrcpy/blob/master/doc/control.md#push-file-to-device)

_¹Double-click on black borders to remove them._  
_²Right-click turns the screen on if it was off, presses BACK otherwise._  
_³4th and 5th mouse buttons, if your mouse has them._  
_⁴For react-native apps in development, `MENU` triggers development menu._  
_⁵Only on Android >= 7._

Shortcuts with repeated keys are executed by releasing and pressing the key a
second time. For example, to execute "Expand settings panel":

 1. Press and keep pressing <kbd>MOD</kbd>.
 2. Then double-press <kbd>n</kbd>.
 3. Finally, release <kbd>MOD</kbd>.

All <kbd>Ctrl</kbd>+_key_ shortcuts are forwarded to the device, so they are
handled by the active application.

For more informations, see [scrcpy](https://github.com/Genymobile/scrcpy).

## Development environment

- OS: Windows 10

- Language: C#

- Framework: .NET Framework 4.7.2

- IDE: Visual Studio
