Huskeregler

- Aldri skriv async void (unntak events)
- Alltid husk hvilken syncronizationcontext du er i
- Async er vanskelig, sjekk ut hva som skjer alltid
	- Testene dine kan kjøre i en annen context enn applikasjonen.
- Bruk alltid ConfigureAwait(false) der du ikke bryr deg om context.
	- Tråd-spesifikke ting vil kunne forsvinne underveis
	- Språk
- TransactionScope «A TransactionScope must be disposed on the same thread that it was created.» -> TransactionScopeAsyncFlowOption 
- Ikke stol på andre.
	- En async metode kan være sync.
	- Biblioteker kan være implementert feil (og mangle ConfigureAwait)
- Bruk Task.Run og ikke Task.Factory.StartNew
- Flere kan vente på samme task.
- Favorisert ‘return task’ over ‘await task’
