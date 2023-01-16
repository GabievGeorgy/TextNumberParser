grammar TextNumbers;

// lexer
fragment A: [aA];
fragment B: [bB];
fragment C: [cC];
fragment D: [dD];
fragment E: [eE];
fragment F: [fF];
fragment G: [gG];
fragment H: [hH];
fragment I: [iI];
fragment J: [jJ];
fragment K: [kK];
fragment L: [lL];
fragment M: [mM];
fragment N: [nN];
fragment O: [oO];
fragment P: [pP];
fragment Q: [qQ];
fragment R: [rR];
fragment S: [sS];
fragment T: [tT];
fragment U: [uU];
fragment V: [vV];
fragment W: [wW];
fragment X: [xX];
fragment Y: [yY];
fragment Z: [zZ];

CHAR:
	A
	| B
	| C
	| D
	| E
	| F
	| G
	| H
	| I
	| J
	| K
	| L
	| M
	| N
	| O
	| P
	| Q
	| R
	| S
	| T
	| U
	| V
	| W
	| X
	| Y
	| Z;

DIGIT:
	Z E R O
	| O N E
	| T W O
	| T H R E E
	| F O U R
	| F I V E
	| S I X
	| S E V E N
	| E I G H T
	| N I N E;
BETWEEN_10_AND_19:
	T E N
	| E L E V E N
	| T W E L V E
	| T H I R T E E N
	| F O U R T E E N
	| F I F T E E N
	| S I X T E E N
	| S E V E N T E E N
	| E I G H T E E N
	| N I N E T E E N;
TENS:
	T W E N T Y
	| T H I R T Y
	| F O R T Y
	| F I F T Y
	| S I X T Y
	| S E V E N T Y
	| E I G H T Y
	| N I N E T Y;
TENS_HYPHEN_DIGIT: TENS '-' DIGIT; // e.g. forty-one
HUNDRED: H U N D R E D (S)?;
THOUSAND: T H O U S A N D (S)?;
MILLION: M I L L I O N (S)?;
BILLION: B I L L I O N (S)?;

NOUGHT: N O U G H T;
POINT: P O I N T;
FRACTIONAL: (NOUGHT WHITESPACE)? (POINT WHITESPACE)+ (
		NOUGHT WHITESPACE
		| DIGIT WHITESPACE?
	)+;

WHITESPACE: ' ';
NEWLINE: '\r'? '\n';
ANY: .;

// parser
content: (number | notNumber)*;
number: (
		billions
		| millions
		| thousands
		| hundreds
		| tensHyphenDigit
		| tens
		| digit
		| fractional
	)+;

digit: DIGIT;
tens: WHITESPACE? (TENS WHITESPACE?) | BETWEEN_10_AND_19;
tensHyphenDigit: TENS_HYPHEN_DIGIT;
hundreds: WHITESPACE? HUNDRED+ WHITESPACE?;
thousands: WHITESPACE? THOUSAND+ WHITESPACE?;
millions: WHITESPACE? MILLION+ WHITESPACE?;
billions: WHITESPACE? BILLION+ WHITESPACE?;
fractional: WHITESPACE? FRACTIONAL;

digitInWord: (CHAR DIGIT) | (DIGIT CHAR);
tensInWord: (CHAR TENS) | (TENS CHAR);
hundredsInWord: (CHAR HUNDRED) | (HUNDRED | CHAR);
thousandsInWord: (CHAR THOUSAND) | (THOUSAND | CHAR);
millionsInWord: (CHAR MILLION) | (MILLION | CHAR);
billionsInWord: (CHAR BILLION) | (BILLION | CHAR);

notNumber: (
		newLine
		| digitInWord
		| tensInWord
		| hundredsInWord
		| thousandsInWord
		| millionsInWord
		| billionsInWord
		| CHAR
		| WHITESPACE
		| POINT
		| NOUGHT
		| ANY
	)+;
newLine: NEWLINE;
