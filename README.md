# BRP-Zadanie-testowe

Zadanie testowe wykonane z idywidualnym podejściem do obsługi inputów.
Gra wykrywa GameState przez co łatwo można operować mapami inputów.

# Sterowanie
Sterowanie jest intuicyjne dla każdego gracza. Gra wykrywa interakcję podłączonego urzadzenia i dostosowuje wyświetlane ikony sugerujące pożądane działanie.
W przypadku gdy ikony nie ma - nie została dodana, nie zostanie w żaden sposó wyświetlona. Podejście te umożliwia w przyszłości bezrpoblemową zmianę bindów bez martwienia się o przypisane ikony na UI czy akcji w kodzie

img width="1052" height="596" alt="image" src="https://github.com/user-attachments/assets/2b41c965-3c2d-489b-8c07-9c3e8f03f06f" />
<img width="1039" height="585" alt="image" src="https://github.com/user-attachments/assets/ab3c205a-a9f6-47f6-8493-87833ff38510" />

Informacje w oparciu o gamepad z playstation:

 Nawigacja:
 D-pad / WSAD
 
 Akcja (gameplay):
 X / Enter
 
 Atak mieczem:
 Kwadrat / Q

 Atak łukiem:
 Kółko / E

 Ekwipunek:
 Trójkąt / I

 Pauza:
 Start / ESC

 Użycie duszy:
 Enter / X

 Usunięcie duszy:
 Kwadrat / Delete

 Powrót:
 Kółko / ESC

 # Wywoływanie inputów
 W projecie uwzględniłem indywidualne podpięcia do istniejących juz przycisków na UI. Przez InputActionReference w łatwy sposób mogłem pozwolić sobie na symulowanie kliknięcia na dany element bez koniecznośći zmiany w kodzie.
 Jest również możliwość podpinania się do eventów z BindControllera co pozwala na obsługe reakcji na poszczególny bind
