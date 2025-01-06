# Unicam.Project
Lo script del Database è il file con il nome "ScriptEsameParadigmi.sql" (qualora si necessitasse dell'username e password, sono entrambi "paradigmi" -> come lo si può anche vedere nel file appsettings.json)

Per quanto riguarda il funzionamento nello swagger: bisogna prima creare un nuovo utente attraverso il "Register", poi con il "Login" (mettendo l'email e la password) verrà generato un Token JWT che permetterà poi di accedere a tutte le altre funzionalità del programma. (N.B. bisogna prima mettere la parola "Bearer" seguita poi dal token che verrà generato in risposta al corretto login dell'utente)

La data dev'essere scritta secondo il formato "YYYY-MM-DD" (qualsiasi altro tipo di formato verrà considerato come errore)

Per quanto riguarda la modifica e l'eliminazione del libro, ho deciso di inserire solo l'id del libro perché era il modo migliore per capire di quale libro si facesse riferimento, e in più, a livello logico, l'id del libro potrebbe essere rappresentato come il suo "codice a barre" (che quindi rappresenta la sua univocità) e dunque ha senso che l'utente sappia quale sia l'id del libro che sta digitando per modificarlo o per eliminarlo dalla sua libreria.
(Ovviamente è anche vero che nelle risposte alla creazione di una categoria e al login di un utente vengono mostrati anche i loro id ma quello è per puro scopo dimostrativo, non verranno mai utilizzati i loro id in nessuna ricerca perché l'utente non ha accesso a queste informazioni)

Mi sono permesso di aggiungere una piccola funzionalità che permette di poter vedere tutte le categorie da noi create.

In più la Modifica di un Libro può anche permettere di non modificare nulla se si lasciasse tutto vuoto, oppure si può modificare solo un campo e il resto può essere tutto vuoto o nullo.

Non si possono mettere alcun tipo di caratteri speciali o numeri all'interno dei vari nomi (che siano di categoria, di editoria, di Autore, ecc...)
