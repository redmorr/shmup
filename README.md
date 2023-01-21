# Sterowanie
* W - góra
* S - dół
* Lewy Przycisk Myszy - strzał

# Package i pluginy
* Input System
* Universal Render Pipeline
* TextMeshPro
* Unity Object Pooling API

# Zewnętrzne assety
* `Singleton.cs` wzięty z darmowego e-booka [Level up your code with game programming patterns](https://blog.unity.com/games/level-up-your-code-with-game-programming-patterns)
* `EnemySpawner.cs` wykorzystany kod z mojego projektu [object pooling class](https://github.com/redmorr/navmesh-agent-simulation/blob/main/Assets/Scripts/Spawning/AgentSpawner.cs)

# Spełnione wymagania

| Wymagania                                                              | Status wykonania | 
|------------------------------------------------------------------------|:----------------:|
| Unity 2021.3                                                           |:white_check_mark:| 
| Najlepszy wynik i wynik z ostatniej rozgrywki                          |:white_check_mark:|
| Rozpoczęcie nowej gry następuje po naciśnięcu dowolnego przycisku      |:white_check_mark:|
| Przesuwający się świat                                                 |       :x:        |
| Gracz porusza się tylko w pione w ograniczonym ekranem zakresie        |:white_check_mark:|
| Przeciwnicy poruszają w lini prostej na jednym z pięciu torów          |:white_check_mark:|
| Przeciwnicy spawnują się w grupach 2-5 w stałych ostępach czasowych    |:white_check_mark:|
| Przeciwników można zestrzelić i zdobyć dzięki temu punkty              |:white_check_mark:|
| Przeciwnicy nie strzelają                                              |:white_check_mark:|
| Utrata życia przy kolizji, koniec gry przy braku życia                 |:white_check_mark:|
| Na ekranie widoczny jest aktualny wynik, ilość żyć oraz czas           |:white_check_mark:|
| Gra jest ograniczona czasowo, koniec gry po upływie czasu              |:white_check_mark:|
| Po zakończeniu pojawia się ekran startowy ze zaktualizowanymi wynikami |:white_check_mark:|
| Najlepszy wynik jest dostępny po ponownym uruchomieniu aplikacji       |:white_check_mark:|

# Czas realizacji

7 godzin nie licząc przerw i tworzenia dokumentacji.

# Wykonanie

Moim celem było zaimplementowanie jak największej ilości wymagań w postaci skryptów odpowiedzialnych tylko za jedną rzecz. W przypadku nie wyrobienia się na czas daje to w pełni funkcjonalną grę z brakującymi featurami, które moża doimplementować poźniej. 

W projekcie nie korzystam z interfejsów, dziedziczenia i Scriptable Objects, ponieważ przy tak małej złożoności (KISS principle) moim zdaniem nie było takiej potrzeby. Gdyby pojawiło się więcej typów i powtórzeń w kodzie np. dodanie kilku rodzajów przeciwników, użycie powyższych było by bardziej uzasadnione.

## Input System

Korzystam z nowego Input System. Obsługiwanie inputu za pomocą eventów z autogenerowanej klasy jest bardzo wygodne, a w edytorze Unity do jednej akcji można dodać wiele rodzajów urządzeń wejściowych. Wykorzystałem tu singletona by móc korzystać z klasy `PlayerControls` w dowolnym miejscu w kodzie i rodzielić logikę sterowania samolotem od logiki strzelania.

## Spawnowanie przeciwników

W klasie `EnemySpawner` wykorzystuję Unity Object Pooling API do ograniczenia alokacji pamięci przez ponownie wykorzystywanie nieaktywnych obiektów przeciwników zamiast wilelokrotnego ich tworzenia. W celu poprawienia wydajności można to rozwiązanie rozszerzyć o przygotowywanie puli przeciwników jeszcze zanim gra się rozpocznie. Chciałem to samo rozwiązanie zastosować w przypadku tworzenia pocisków, ale nie starczyło mi na to czasu.

## Liczenie punktów
Po zestrzeleniu lub zderzeniu z przeciwnikiem, `Enemy` wywołuje statyczny event, który `ScorePresenter` obserwuje i odpowiednio aktualizje wynik.

## Zarządzanie stanem gry

`GameManager` odpowiada za zarządzaniem flow gry. Swoje dependencje otrzymuje z inspektora. Klasa jest Singletonem który nie jest niszczony po ładowaniu sceny. Dało by możliwość ewentualnego resetu sceny po zakończeniu gry, ale dzięki wyłączaniu i przenoszniu obiektów na miejsca startowe nie musiałem z tego korzystać. Możliwe, że lepszym rozwiązaniem byłoby wywoływać eventy `GameStart` i `GameOver` zamiast wywoływania metod z klas, które są dependencjami.

## UI

Do interakcji z UI wykorzystuję wzorzec MVP wszędzie oprócz GameManagera, gdzie niestety nie przeniosłem licznika czasu do nowej klasy. Z braku czasu najwyższy i ostatni wynik, wyniki widać nie tylko w na ekranie startowym. ale także w trakcie rozgrywki. Do przechowywania wyników korzystam z `PlayerPrefs`.

Nie ma zaimplementowanego przewijania się mapy, jest tylko statyczne tło.

