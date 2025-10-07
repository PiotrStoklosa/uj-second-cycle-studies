describe("Utility Functions", function () {
    it("Potega_PositiveFlag_PositiveExponent_ReturnsPower", function () {
        expect(Potega(2, 3, 1)).toBe(8);
    });

    it("Potega_PositiveFlag_NegativeExponent_ReturnsErrorMessage", function () {
        expect(Potega(2, -1, 1)).toBe("wykładnik mniejszy od 0");
    });

    it("Potega_NonPositiveFlag_ReturnsErrorMessage", function () {
        expect(Potega(2, 3, 0)).toBe("trzeci argument jest mniejszy od 0");
    });

    it("ZapiszWTablicy_MultipliesElements_ReturnsNewArray", function () {
        expect(ZapiszWTablicy([1, 2, 3], 2)).toEqual([2, 4, 6]);
    });

    it("PoleKola_PositiveRadius_ReturnsArea", function () {
        expect(PoleKola(2)).toBeCloseTo(Math.PI * 4);
    });

    it("PoleKola_NonPositiveRadius_ReturnsErrorMessage", function () {
        expect(PoleKola(-1)).toBe("promień musi być większy od 0");
    });

    it("SumaCyfr_ValidThreeDigitNumber_DivisibleByThree_ReturnsMessage", function () {
        expect(SumaCyfr(123)).toBe("liczba podzielna przez 3");
    });

    it("SumaCyfr_ValidThreeDigitNumber_NotDivisibleByThree_ReturnsMessage", function () {
        expect(SumaCyfr(124)).toBe("liczba nie jest podzielna przez 3");
    });

    it("SumaCyfr_InvalidNumber_ReturnsErrorMessage", function () {
        expect(SumaCyfr(99)).toBe("podana liczba nie jest trzycyfrowa");
    });

    it("ZamienElementy_ValidIndicesAndPositiveFlag_SwapsElements", function () {
        expect(ZamienElementy([1, 2, 3], 0, 2, 1)).toEqual([3, 2, 1]);
    });

    it("ZamienElementy_InvalidIndices_ReturnsErrorMessage", function () {
        expect(ZamienElementy([1, 2, 3], -1, 2, 1)).toBe("indeks spoza zakresu");
    });

    it("ZamienElementy_NonPositiveFlag_ReturnsOriginalArray", function () {
        expect(ZamienElementy([1, 2, 3], 0, 2, 0)).toEqual([1, 2, 3]);
    });
});
