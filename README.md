# ElectricFox.BdfSharp - C# BDF Font Loader and Renderer

## Introduction

This library provides tools for loading and rendering BDF font files in C#.

I originally built it because I had a collection of BDF fonts and wanted to use them in projects such as:

- Adding text to pixel art
- Rendering text to small displays (e.g. ePaper screens)

Many of the fonts I tested weren't strictly compliant with the BDF spec, so this library is designed to be forgiving when parsing. It also provides flexible glyph lookup options to handle unusual encodings, missing mappings, and even icon-style glyphs with no standard encoding.

# Useage

## Loading a font

```
var font = BdfFont.Load("some-bdf-font.bdf");
```

## Measuring text

This library includes the ability to measure the size of rendered text in pixels.

```
var rect = font.MeasureString("Hello, World!");
```

The MeasureString method returns a rectangle that describes both the size and the position of the text relative to its baseline. Unlike simple width/height measurements, this rectangle also includes an X and Y offset that indicate where the text sits in relation to the origin point (typically the baseline at X = 0, Y = 0).

This extra positioning information is important for precise alignment: it allows you to line up text on a shared baseline rather than using the arbitrary "top edge" of the bounding box, which can vary depending on the font or glyphs used.

For example, a call to `MeasureString("Qu")` might return a rectangle with values:

```
X = 0
Y = -15
Width = 29
Height = 19
```

Here, the red cross marks the text’s origin (0,0). The negative Y value indicates that part of the text extends above the baseline, while the height ensures that descenders (like in g or y) would also be captured correctly.

![Example showing the position of two letters with the origin highlighted](https://github.com/ashleyhuxley/bdfsharp/blob/main/docs/qu-example.png)

## Rendering Text

You can render text into a two-dimensional pixel array using `RenderBitmap`.
The array dimensions will match the rectangle returned by `MeasureString`.

```
var data = font.RenderBitmap("Hello, World!");

for (int x = 0; x < data.GetLength(0); x++)
{
    for (int y = 0; y < data.GetLength(1); y++)
    {
        g.FillRectangle(data[x, y] ? Brushes.Black : Brushes.White, x, y, 1, 1 );
    }
}
```

## Looking up glyphs

BDF fonts often contain unusual or inconsistent encodings.
To make lookups more reliable, this library uses a multi-stage fallback strategy when resolving glyphs for characters:

1. A glyph with an `ENCODING` value matching the requested code point
2. A glyph whose `STARTCHAR` name is a single character matching the requested code point
3. A glyph with a name in the form `uniABCD` or `U+ABCD` that matches the Unicode value

You can control this behaviour with the `GlyphLookupOption` enum:

- `EncodingStrict` – Only use the `ENCODING` field. No fallbacks.
- `BestGuess` (default) – Try `ENCODING` first, then fall back to names when possible.
- `UseIndex` – When all else fails, fallback to the glyph’s index position in the font.
