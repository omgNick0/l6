#include <iostream>
#include <string>
#include <vector>
#include <limits> 
using namespace std;

class ShipBase {
protected:
    double length; 

public:
    ShipBase(double length) : length(length) {}

    virtual void displayInfo() const = 0; 
    virtual void modify() = 0;           
    virtual ~ShipBase() {}

    double getLength() const { return length; } 

    template <typename T>
    static T getValidatedInput(const string& prompt) {
        T value;
        while (true) {
            cout << prompt;
            cin >> value;
            if (cin.fail()) {
                cin.clear();
                cin.ignore(numeric_limits<streamsize>::max(), '\n');
                cout << "Некорректный ввод. Попробуйте ещё раз.\n";
            }
            else {
                return value;
            }
        }
    }

    friend void compareLengths(const ShipBase& ship1, const ShipBase& ship2);
};


class Ship : public ShipBase {
protected:
    string name;
    int crew;

public:
    Ship(string name, double length, int crew)
        : ShipBase(length), name(name), crew(crew) {}

    void displayInfo() const override {
        cout << "Название: " << name << endl;
        cout << "Длина: " << length << " метров" << endl;
        cout << "Экипаж: " << crew << " человек" << endl;
    }

    void modify() override {
        cout << "Введите новое название: ";
        cin >> name;
        length = getValidatedInput<double>("Введите новую длину (в метрах): ");
        crew = getValidatedInput<int>("Введите новый размер экипажа: ");
    }
};


class Steamship : virtual public Ship {
private:
    int enginePower;

public:
    Steamship(string name, double length, int crew, int enginePower)
        : Ship(name, length, crew), enginePower(enginePower) {}

    void displayInfo() const override {
        Ship::displayInfo();
        cout << "Мощность двигателя: " << enginePower << " л.с." << endl;
    }

    void modify() override {
        Ship::modify();
        enginePower = getValidatedInput<int>("Введите новую мощность двигателя (л.с.): ");
    }
};


class SailingShip : virtual public Ship {
private:
    int sails;

public:
    SailingShip(string name, double length, int crew, int sails)
        : Ship(name, length, crew), sails(sails) {}

    void displayInfo() const override {
        Ship::displayInfo();
        cout << "Количество парусов: " << sails << endl;
    }

    void modify() override {
        Ship::modify();
        sails = getValidatedInput<int>("Введите новое количество парусов: ");
    }
};


class Yacht : public SailingShip, public Steamship {
private:
    string luxuryLevel;

public:
    Yacht(string name, double length, int crew, int sails, string luxuryLevel, int enginePower)
        : Ship(name, length, crew), SailingShip(name, length, crew, sails), Steamship(name, length, crew, enginePower), luxuryLevel(luxuryLevel) {}

    void displayInfo() const override {
        SailingShip::displayInfo();
        cout << "Уровень роскоши: " << luxuryLevel << endl;
    }

    void modify() override {
        SailingShip::modify();
        cout << "Введите новую мощность двигателя (л.с.): ";
        int newEnginePower;
        cin >> newEnginePower;

        cout << "Введите новый уровень роскоши: ";
        cin >> luxuryLevel;
    }
};

// Дружественный метод для сравнения длин двух кораблей
void compareLengths(const ShipBase& ship1, const ShipBase& ship2) {
    cout << "Сравнение длин кораблей:" << endl;
    if (ship1.getLength() > ship2.getLength()) {
        cout << "Первый корабль длиннее." << endl;
    }
    else if (ship1.getLength() < ship2.getLength()) {
        cout << "Второй корабль длиннее." << endl;
    }
    else {
        cout << "Оба корабля имеют одинаковую длину." << endl;
    }
}

void showMenu() {
    cout << "\nУправление кораблями:" << endl;
    cout << "1. Добавить новый корабль" << endl;
    cout << "2. Изменить характеристики корабля" << endl;
    cout << "3. Показать все корабли" << endl;
    cout << "4. Удалить корабль" << endl;
    cout << "5. Сравнить длины двух кораблей" << endl;
    cout << "6. Выход" << endl;
    cout << "Введите ваш выбор: ";
}


ShipBase* createShip() {
    int choice = Ship::getValidatedInput<int>("Выберите тип корабля:\n1. Обычный Корабль\n2. Пароход\n3. Парусник\n4. Яхта\nВведите номер: ");

    string name;
    double length;
    int crew;

    cout << "Введите название: ";
    cin >> name;
    bool flag = true;
    while(flag) {
        length = Ship::getValidatedInput<double>("Введите длину (в метрах): ");
        if (length < 10) {
            cout << "Корабль не может быть меньше 10 метров!" << endl;
            continue;
        } else if (length > 180) {
            cout << "Самой большой в мире корабль 180 метров в длину! Вряд ли у Вас получится создать бОльший" << endl;
            continue;
        }
        crew = Ship::getValidatedInput<int>("Введите размер экипажа: ");

        if (crew <= 0) {
            cout << "Кол-во экипажа не может быть отрицательным или равняться нулю!" << endl;
            continue;
        } else if (crew > 20 && (choice != 4 && (length < 50 || length >= 100))) {
            cout << "Больше 30 человек можно расположить только на яхте длиной более 50 метров, либо на корабле длиной более 100 метров" << endl;
            continue;
        }

        flag = false;

    } 

    if (choice == 1) {
        return new Ship(name, length, crew);
    }
    else if (choice == 2) {
        int enginePower = Ship::getValidatedInput<int>("Введите мощность двигателя (л.с.): ");
        return new Steamship(name, length, crew, enginePower);
    }
    else if (choice == 3) {
        int sails = Ship::getValidatedInput<int>("Введите количество парусов: ");
        return new SailingShip(name, length, crew, sails);
    }
    else if (choice == 4) {
        int sails = Ship::getValidatedInput<int>("Введите количество парусов: ");
        int enginePower = Ship::getValidatedInput<int>("Введите мощность двигателя (л.с.): ");
        string luxuryLevel;
        cout << "Введите уровень роскоши: ";
        cin >> luxuryLevel;
        return new Yacht(name, length, crew, sails, luxuryLevel, enginePower);

    }
    else {
        cout << "Неверный выбор!" << endl;
        return nullptr;
    }
}


int main() {
    setlocale(LC_ALL, "Russian");
    vector<ShipBase*> ships;
    int choice;

    do {
        showMenu();
        choice = Ship::getValidatedInput<int>("");

        switch (choice) {
        case 1: {
            ShipBase* newShip = createShip();
            if (newShip) {
                ships.push_back(newShip);
                cout << "Корабль успешно добавлен!" << endl;
            }
            break;
        }
        case 2: {
            if (ships.empty()) {
                cout << "Список кораблей пуст." << endl;
                break;
            }
            int index = Ship::getValidatedInput<int>("Введите номер корабля для изменения (0 до " + to_string(ships.size() - 1) + "): ");
            if (index >= 0 && index < ships.size()) {
                ships[index]->modify();
                cout << "Характеристики изменены!" << endl;
            }
            else {
                cout << "Неверный номер!" << endl;
            }
            break;
        }
        case 3: {
            if (ships.empty()) {
                cout << "Список кораблей пуст." << endl;
            }
            else {
                for (size_t i = 0; i < ships.size(); ++i) {
                    cout << "Корабль #" << i << ":\n";
                    ships[i]->displayInfo();
                    cout << "-----------------------" << endl;
                }
            }
            break;
        }
        case 4: {
            if (ships.empty()) {
                cout << "Список кораблей пуст." << endl;
                break;
            }
            int index = Ship::getValidatedInput<int>("Введите номер корабля для удаления (0 до " + to_string(ships.size() - 1) + "): ");
            if (index >= 0 && index < ships.size()) {
                delete ships[index];
                ships.erase(ships.begin() + index);
                cout << "Корабль удалён!" << endl;
            }
            else {
                cout << "Неверный номер!" << endl;
            }
            break;
        }
        case 5: {
            if (ships.size() < 2) {
                cout << "Для сравнения нужно как минимум два корабля." << endl;
                break;
            }
            int index1 = Ship::getValidatedInput<int>("Введите номер первого корабля (0 до " + to_string(ships.size() - 1) + "): ");
            int index2 = Ship::getValidatedInput<int>("Введите номер второго корабля (0 до " + to_string(ships.size() - 1) + "): ");
            if (index1 >= 0 && index1 < ships.size() && index2 >= 0 && index2 < ships.size()) {
                compareLengths(*ships[index1], *ships[index2]);
            }
            else {
                cout << "Неверные номера!" << endl;
            }
            break;
        }
        case 6:
            cout << "Выход из программы." << endl;
            break;
        default:
            cout << "Неверный выбор! Попробуйте ещё раз." << endl;
        }
    } while (choice != 6);

    for (ShipBase* ship : ships) {
        delete ship;
    }

    return 0;
}