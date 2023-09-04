<table border="1" width="204" id="table1" cellspacing="0" cellpadding="0"> 
<tr> 
<td width="5%">cb</td> 
<td width="31%" colspan="2">text</td> 
</tr>
<tr> 
<td width="11%" colspan="2" rowspan="2">icon</td> 
<td width="89%">empty</td> 
</tr> 
<tr> 
<td width="89%">input</td> 
</tr> 
</table>

===================================
PART 1 of 7 from Dave H.

Hi Becky,

I will skip your original markup. It appears a few times above.

Please do not take offense in any way of feel all of my comments are directed at your code specifically.  In designing for HTML it is important to clearly and explicitly state what you want the browser client to do.  
I do not believe your alignment problems is related to either the use of pixels or percentages.

Though you are using your tags in the intended manner the pitfall you are falling into is that you are asking the browser to interpret your layout and make decisions about what you intended.  Not only is it possible for the browsers to have slightly different interpretations, but as you have seen, different features of say row handling, where percentages or used versus pixels might come out with slightly different rules for interpretations, and this is on a the one browser one version tests you are encountering a problem with.

HTML is far more forgiving that XHTML.  This is sometimes good in that it continues despite inconsistencies, better than crapping out with an error.  However, in designing you do not want to let the various client browsers be able to make these decisions on there own.  You want to be explicit, thorough and consistent.

===================================
PART 2 of 7 from Dave H.
Lets examine your mark up:
-------------------------------------
<table border="1" width="204" id="table1" cellspacing="0" cellpadding="0">
-------------------------------------
A TABLE 204 PIXELS WIDE
-------------------------------------
THE TABLE HAS 3 ROWS
-------------------------------------
ROW 1 <tr> 
-------------------------------------
CELL 1 <td  
-------------------------------------
CELL 2 <td width="31%" COLSPAN="2">text</td>  
-------------------------------------
ROW 1 HAS TWO CELLS
-------------------------------------
ROW 1 </tr> 
-------------------------------------
ROW 2 <tr> 
-------------------------------------
CELL 1<td  
-------------------------------------
CELL 1<td 
-------------------------------------
THIS ROW HAS TWO CELLS
-------------------------------------
ROW 2 </tr> 
-------------------------------------
ROW 3 <tr>
-------------------------------------
CELL 1<td 
-------------------------------------
THIS ROW HAS ONE CELL
-------------------------------------
ROW 3 </tr> 
-------------------------------------
</table>
------------------------------------
===================================
PART 3 of 7 from Dave H.

DECISION 1
==========
Tables Consist of row and columns.  If we look at ROW1 CELL2 it has a COLSPAN of 2.
Does this mean that this row has three columns or two?  Technically, the answer is three columns.

ROW 3 has one cell.
Does the table therefore have three (ROW1), two (ROW2), or one (ROW3) columns.  The answer is the largest number 3.  It has 1 cell (by default, 1 column wide) plus 1 cell two columns wide.

That may be open to debate but I if feel my interpretation is the correct one.

===================================
PART 4 of 7 from Dave H.

DECISION 2
==========
First the information in your markup:
------------------------------------
<table border="1" width="204" id="table1" cellspacing="0" cellpadding="0"> 
------------------------------------
ROW 1<tr> 
------------------------------------
COLUMN 1.......................<td width=	"5%"
------------------------------------ 
COLUMN 2 & 3.................<td width=	"31%
------------------------------------
TOTAL WIDTH OF ROW 1 = 36%
------------------------------------ 
ROW 1</tr> 
------------------------------------
ROW 2<tr> 
------------------------------------
COLUMN 1.......................<td width="	11%"
------------------------------------ 
COLUMN 2.......................<td width="     89%"
------------------------------------ 
ROW 2</tr> 
------------------------------------
TOTAL WIDTH OF ROW 2 = 100%
------------------------------------
ROW 3<tr> 
------------------------------------
COLUMN 1.......................<td width="	89%
------------------------------------ 
ROW 3</tr> 
------------------------------------
TOTAL WIDTH OF ROW 3 = 89%
------------------------------------
</table>
------------------------------------
===================================
PART 5 of 7 from Dave H.

When looking at three rows, row two adds up to 100% so if guess that would be a strong clue that the browser would latch on to. At the same time, Row 3, Column 1 = 89%, which is the same as Row 2, Column 2, so maybe these two columns should exist over top of each other and should align using a right justified strategy.  Probably not.

Left justified is the default as you know so the default behavior would have whatever the number of columns and cells the total percentages would be the most reliable guide and as you can see, none of them do in fact line up.

Both myself and your browsers are having to make some decisions about what you really intended, which as you said, is that these columns should line up.

I am now going two write out what I think you might have meant and in your next post you can correct me where I misunderstood.  The table width? Did you intend it to be 200 pixels but the 2 pixel padding was causing problems?
===================================
PART 6 of 7 from Dave H.
------------------------------------
TABLE 1
------------------------------------
<table border="1" width="200" id="table1" cellspacing="0" cellpadding="0"> 
------------------------------------
ROW 1
------------------------------------
<tr> 
------------------------------------
COLUMN 1
------------------------------------
<td width="10px">cb</td> 
------------------------------------
COLUMN 2
------------------------------------
<td width="170px%">text</td>
------------------------------------
</tr> 
------------------------------------
ROW 2
------------------------------------
<tr> 
------------------------------------
COLUMN 1 
------------------------------------
<td width="22px">icon</td>
------------------------------------
COLUMN 2 - ALIGNED TOP LEFT
------------------------------------
<td width="178px">input</td> 
------------------------------------
</tr> 
------------------------------------
</table>
------------------------------------
===================================
PART 7 of 7 from Dave H.

Question: For row 2, column 1, what is the size of icon you wanted here? 16px? 32px?
I think you might have wanted a 32 pixel icon plus 2 pixel padding on all sides?

We now have a table with 2 rows and two columns.  The columns widths of each row are equal and equal to the width of the table.  Remember that tables should always thought of as grid paper.

If you mean to build this sophisticated layouts not made up of rows of file output then we are really talking about blocked out areas.

You will be best served to think of text output as <P>aragraphs where special formatting of part of a sentence is a <SPAN> of decorated text.

It is not bad to use tables to block out large regions of specific size but think of it as grid paper where all columns are of equal size and use your colspans so that every row is very has an equal number of columns.

If you are talking about layouts then use clearly expressed "DIV"s to define express the position and size of each area in pixels I suppose and then place your paragraphs, images and input fields inside.

Remember, all of part 7 goes in the HTML document.  They are context.  The exact positions, layout, size, text fonts and decorations should go in the CSS style sheet document or area.  The presentation and formating, or style if you prefer.

Regards,
Dave H.
