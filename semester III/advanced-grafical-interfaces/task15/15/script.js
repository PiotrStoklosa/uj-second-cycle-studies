function Potega(baseValue, exponent, flag) {
    if (flag > 0) {
        if (exponent < 0) {
            return "wykładnik mniejszy od 0";
        }
        return Math.pow(baseValue, exponent);
    }
    return "trzeci argument jest mniejszy od 0";
}

function ZapiszWTablicy(array, multiplier) {
    return array.map(element => element * multiplier);
}

function PoleKola(radius) {
    if (radius <= 0) {
        return "promień musi być większy od 0";
    }
    return Math.PI * Math.pow(radius, 2);
}

function SumaCyfr(number) {
    if (number >= 100 && number <= 999) {
        let sum = number.toString().split('').reduce((acc, digit) => acc + parseInt(digit), 0);
        return sum % 3 === 0 ? "liczba podzielna przez 3" : "liczba nie jest podzielna przez 3";
    }
    return "podana liczba nie jest trzycyfrowa";
}

function ZamienElementy(array, index1, index2, flag) {
    if (flag > 0) {
        if (index1 < 0 || index1 >= array.length || index2 < 0 || index2 >= array.length) {
            return "indeks spoza zakresu";
        }
        [array[index1], array[index2]] = [array[index2], array[index1]];
    }
    return array;
}

function displayOutput(text) {
    document.getElementById("output").innerHTML += `<p>${text}</p>`;
}

// Przykładowe wywołania
displayOutput(Potega(2, 3, 1));
displayOutput(PoleKola(5));
displayOutput(SumaCyfr(123));
displayOutput(JSON.stringify(ZamienElementy([10, 20, 30], 0, 2, 1)));