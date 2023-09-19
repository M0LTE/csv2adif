# csv2adif

A utility which transforms a simple CSV into an ADIF file, utilising M0LTE's `adiflib` package.

Compiled binaries for multiple platforms are present under https://github.com/M0LTE/csv2adif/releases. These should have no major dependencies, although they aren't really tested as yet.

Issue reports and PRs most welcome. Please raise issues against https://github.com/M0LTE/adiflib if it's not specific to csv2adif. If you're not sure, just raise it here.

## Sample input file

```
Callsign,Frequency,Mode,TX-RST,RX-RST,UTC-On,UTC-Off,Operator,Station,Date,Power,StationLocator,Locator
M0LTE,7.029,CW,599,599,155059,155117,GM5AUG,GM5AUG/P,20230919,60,IO74LD,IO91LK
G3CO,7.029,CW,589,579,155127,155134,GM5AUG,GM5AUG/P,20230919,60,IO74LD,
```

## Usage

./csv2adif myinputfile.csv > output.adif

## Sample output file

```
<programid:7>ToppLog
<eoh>

<call:5>M0LTE
<freq:5>7.029
<mode:2>CW
<rst_sent:3>599
<rst_rcvd:3>599
<operator:6>GM5AUG
<station_callsign:8>GM5AUG/P
<tx_pwr:2>60
<qso_date:8>20230919
<time_on:6>165059
<qso_date_off:8>20230919
<time_off:6>165117
<gridsquare:6>IO91LK
<my_gridsquare:6>IO74LD
<eor>

<call:4>G3CO
<freq:5>7.029
<mode:2>CW
<rst_sent:3>589
<rst_rcvd:3>579
<operator:6>GM5AUG
<station_callsign:8>GM5AUG/P
<tx_pwr:2>60
<qso_date:8>20230919
<time_on:6>165127
<qso_date_off:8>20230919
<time_off:6>165134
<my_gridsquare:6>IO74LD
<eor>
```
