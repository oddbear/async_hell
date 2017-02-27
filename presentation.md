Huskeregler

- Aldri skriv async void (unntak events)
- Alltid husk hvilken syncronizationcontext du er i
- Async er vanskelig, sjekk ut hva som skjer alltid
	- Testene dine kan kj�re i en annen context enn applikasjonen.
- Bruk alltid ConfigureAwait(false) der du ikke bryr deg om context.
	- Tr�d-spesifikke ting vil kunne forsvinne underveis
	- Spr�k
- TransactionScope �A TransactionScope must be disposed on the same thread that it was created.� -> TransactionScopeAsyncFlowOption 
- Ikke stol p� andre.
	- En async metode kan v�re sync.
	- Biblioteker kan v�re implementert feil (og mangle ConfigureAwait)
- Bruk Task.Run og ikke Task.Factory.StartNew
- Flere kan vente p� samme task.
- Favorisert �return task� over �await task�
