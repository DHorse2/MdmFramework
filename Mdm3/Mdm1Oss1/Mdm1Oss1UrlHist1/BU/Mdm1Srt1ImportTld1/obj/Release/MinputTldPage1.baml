   M S B A M L   `   `   `  ����   Mdm1Srt1ImportTld1%    Mdm.Srt.InputTld.MinputTldPage1   W SPresentationCore, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    8x,http://schemas.microsoft.com/winfx/2006/xaml    -   $��	Tld1Page1��  J� 	AllowDrop  .   N� FontSize$ 10(�$	��400��$	��400��$
��1200��$
��1500�� N� 	KeepAlive .  N� Title$' !MacroDm Tilde File Import Utility�� � ToolTip$A ;This application imports and converts files into a database��$
��Left=�$��Stretch=� N� WindowTitle$2 ,MacroDm Tilde File Multi-Item Import Utility�� N� ShowsNavigationUI .  � IsSharedSizeScope .  |�. .���� -   $��PageScrollViewer��$
��Auto=�$
��Auto=�$��	Condensed&� .  $
��1500��$
��1200��.��� -   $��PageGridMain�� � ShowGridLines .  $; 5Check off any options you want to use on this import.��  .  ��.  .  $
��1500��$
��1200��	 � RowDefinitions	 �� -   
 �Name$
 gcRow0��$��45 �$��30��$��55�� z� SharedSizeGroup$ TldHeaderRows���� -   $
 gcRow1��$��30 �$��30��$	��180��$ TldFileRows���� -   $
 gcRow2��$��30 �$��30��$	��180��$ TldFileRows���� -   $
 gcRow3��$��30 �$��30��$	��180��$ TldFileRows���� -   $
 gcRow4��$��20 �$��20��$��40��$ TldOptionRows���� -	   $
 gcRow5��$��20 �$��20��$��40��$ TldOptionRows���� -
   $
 gcRow6��$��20 �$��20��$��40��$ TldOptionRows���� -   $
 gcRow7��$��20 �$��20��$��40��$ TldOptionRows���� -   $
 gcRow8��$��40 �$��35��$��60��$ TldButtonRows���� -   $
 gcRow9��$��40 �$��35��$��60��$ TldButtonRows���� -   $
 gcRow10��$��* �$��30��$	��175��$ TldConsoleRows���� -   $
 gcRow11��$��* �$��30��$��75��$ TldConsoleRows���� -   $
 gcRow12��$��* �$��30��$��75��$ TldConsoleRows���� -   $
 gcRow13��$��15 �$��5��$��35��$ TldFooterRows�� � ColumnDefinitions �� -   $
 gcCol0��$	��150 �$	��150���� -   $
 gcCol1��$��45 �$��45���� -   $
 gcCol2��$	��100 �$	��100���� -   $
 gcCol3��$	��100 �$	��100���� -   $
 gcCol4��$	��170 �$	��170���� -   $
 gcCol5��$��* �$��25��$	��800���� -   $
 gcCol6��$��5 �$��5��$��5��$ TldBorderCols��.+��� -   $��ApplicationHeading1��$��5,5,0,5q�$��16(�$��Center=�$��Center=�$��	Condensed&�$��1q�$	��550��$
��Left=�$	��550��$��0��$��5��  . .�� MacroDm Import File Processing�� -   $��InputFileNameLabel��}�.  $��1��$��Right=�$��Center=�$��14(�$��1q�|�.  $��2��$� �Enter the file that will be processed.  \n\rNormal a text file with optional TLD format row / records names (i.e. ~me~) \n\rwhen there are multiple records in the file.��$��	Condensed&�$��84.873,8,10,4q�  .  .��Input File Name�� -   $��InputFileLine�� �� TextAlignment$
 Left=�$��12(�$	��120��$��1��$� �Enter the file that will be processed.  \n\rNormal a text file with optional TLD format row / records names (i.e. ~me~) \n\rwhen there are multiple records in the file.��$��2�� .  $��25�� �� TextWrapping$ WrapWithOverflow=� � Cursor$ IBeam��$��4��$��Center=�}�.   �� MaxLines$ 3�� �� 	UndoLimit$ 15��" �� VerticalScrollBarVisibility$
 Auto=� �� AcceptsReturn .   �� 	MaxLength$	 512��$��Center=�$��25��$��0,0,5,0q�  . �� -   $��OutputFileNameLabel��$��2��$��Right=�$��Center=�$��0,0,10,0q�$��12(�$��Center=�}�.  |�.  $��2��$��1q�$��Right=�  .  .��Output File Name�� -   $��OutputFileLine��$
 Left=�$��12(�$	��120��$��2��$��2�� .  $��25��$ WrapWithOverflow=�$ IBeam��$��4��$��Center=�$��Center=�}�.  $ 3��$
 Auto=� .  $	 512��$��25��$ 15��$��0,0,5,0q��� -   $��OutputIdLabel��$��3��$��12(�$��Right=�$��Center=�$��Center=�}�.  |�.  $��0��$��2��$��0,0,10,0q�$��	Condensed&�$��1q�$��Right=�  .  .��	Output Id�� -   $��OutputFileItemId��$��2��$��3��$��12(�$	��120��$��4��$��0q�$��Center=� � ForceCursor .  $ 15��}�.  $	 512��$��25��$ 3��$
 Auto=�$ WrapWithOverflow=�$ IBeam��$��Center=�$��25��$��0,0,5,0q��� -    $��OptionsLabel��$��4��$��Right=�$��Center=�$��Center=�}�.  |�.  $��0��$��25��$��2��$��1q�$��0,0,10,0q�$��	Condensed&�$��Right=�  .  .��	Options�� -!   $%��OptionToDoOverwriteExistingItem��$
��Left=�$��Center=�$��2��$��4�� .   � OverridesDefaultStyle .  $��3,0,0,0q�}�.  $��2��$��1��$��Center=�$
��Left=�$��0q�.��Overwrite Existing Items�� -"   $#��OptionToDoCheck_FileDoesExist��$
��Left=�$��Center=�$��2��$��5�� .   .  $��Center=�$��3,0,0,0q�}�.  $��2��$
��Left=�.��Check Files Already Exist�� -#   $��OptionToDoCheck_ItemIds��$
��Left=�$��Center=�$��2��$��6�� .   .  $��Center=�$��3,0,0,0q�}�.  $��2��$
��Left=�.��#!Confirm Item Id already DoesExist�� -$   $��OptionToDoEnterEachItemId��$��2��$��7��$
��Left=�$��Center=� .   .  $��3,0,0,0q�}�.  $��2��$��Center=�$
��Left=�.��Enter Each Item Id�� -%   $��OptionToDoLogActivity��$
��Left=� .  $��3,0,0,0q�$��Center=�$��4��$��4��$��Center=�}�.  $��15.96��$��1.44,0,11.253,0q�.��Log Activity�� -&   $$��OptionToDoProceedAutomatically��$
��Left=� .  $��3,0,0,0q�$��Center=�$��4��$��5��$��Center=�}�.  $��15.96��$��1.44,0,11.253,0q�.��Proceed Automatically�� -'   $!��OptionToDoCreateMissingFile��$
��Left=� .  $��3,0,0,0q�$��Center=�$��4��$��6��$��Center=�}�.  $��15.96��$��1.44,0,11.253,0q�.��Create Missing Output File�� $	��550�� �� Orientation$ 
Horizontal=�$��8��$��40��$��20��$��60��$	��550��$��5��  .  $��0q�$��1��.+��� -(   $��DatabasePageButton��$��	195,5,5,5q�$��Center=�}�.  $��1q�$��10(�$��Center=�$��Center=�$��65��$��20��$��	Condensed&�$��75��$��25�� �� 	IsDefault .    .  $��65��$��25��.��
Database�� -)   $��OptionButtonPressed��$��25,5,5,5q�$��Center=�}�.  $��1q�$��10(�$��Center=�$��Center=�$��65��$��20�� . $��	Condensed&�$��75��$��25��  .  $��65��$��25��.��	Details�� $	��550��$ 
Horizontal=�$��9��$��40��$��20��$��60��$	��550��$��5��  .  $��0q�$��1��.+�� -*   $��ProgressBar1��$��10q���.   � Maximum$ 1000000Q�$��15��$��Center=�$��Stretch=�$	��150��  .  
��� � ��� -+   $��StartButtonPressed��$��25,5,5,5q�$��Center=�}�.  $��1q�$��10(�$��Center=�$��Center=�$��65��$��20�� . $��	Condensed&�$��75��$��25��  .  $��65��$��25��.��Start�� -,   $��PauseButtonPressed��$��25,5,5,5q�$��Center=�}�.  $��1q�$��10(�$��Center=�$��Center=�$��65��$��20�� . $��	Condensed&�$��75��$��25��  .  $��65��$��25��.��Pause�� --   $��CancelButtonPressed��$��25,5,5,5q�}�.  $��10(�$��1q�$��65��$��Center=�$��Center=�$��20�� �� IsCancel . $��	Condensed&�$��75��$��25��  .  $��65��$��25��.��Cancel�� -.   $��ConsoleCommandlabel��$��10��$��5q�$��Right=�$��1��$	��Top=�$��3q�$��Right=�  .  .��Console Command�� -/   $��ConsoleCommandText��$��5��$��10��$��5,6,0,4q�$	��Top=�$��1��$��25��$	��120�� .  $
 Auto=�$ NoWrap=�$ �� HorizontalScrollBarVisibility$
 Auto=�$
 2500�� .  �� 
AcceptsTab . $��MonospaceTypewriter)�$��Normal&��� -0   $��StatusLine1��$��11��$��1��$��5��|�. $��5,5,0,5q�$��25��$	��120��$
 Auto=�$ WrapWithOverflow=�  .  �� -1   $��StatusLine2��$��1��$��5��$��12��$��5,5,0,5q�|�. $��25��$	��120��$
 Auto=�$ WrapWithOverflow=�  .  