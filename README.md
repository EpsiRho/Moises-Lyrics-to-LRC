# Moises-Lyrics-to-LRC

This script will take a json file taken from Moises's AI Lyrics Editor and convert it to LRC fomrat.

It can write either standard per-line timestamped LRC or per-word timestamped A2 LRC.

The code will ask you for input on running, or you can supply it launch arguments from your console.

## Example launch arguments

`>MoisesLyricsToLRC.exe -perWord -jsonPath C:\Users\Epsi\Desktop\lyrics.json`

`>MoisesLyricsToLRC.exe -perLine -jsonPath C:\Users\Epsi\Desktop\lyrics.json`

## Where to get Moises JSON

Moises doesn't just let you download it easy. 
- Navigate to the song you want the lyrics from.
- Hit F12 to open your browser's DevTools.
- In here, navigate to the "Network" tab
- Back on Moises, Hit the lyrics button, and then "Edit Lyrics"
- The DevTools should now have items in the Netowrk tab.
- You'll be looking for JSON files, usually there are 3 here.
- You'll be looking for the one with a random guid, somehthing like: 2dfbbbc4-2be4-4154-b860-b0100af69c96
- The json output that looks likes like the following, and can be found by clicking on the name and then the sub-tab "Response"
``` JSON
{
  "id": 2,
  "line_id": 1,
  "start": 160.50115646258502,
  "end": 160.70530612244897,
  "text": "You",
  "confidence": "0.97"
},
```
- Copy all of the JSON and put it somewhere on your PC and name it anything.
- Run MoisesLyricsToLRC.exe and point it to the JSON file you saved.
