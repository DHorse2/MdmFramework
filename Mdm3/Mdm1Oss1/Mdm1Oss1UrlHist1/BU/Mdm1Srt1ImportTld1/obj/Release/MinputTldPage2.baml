   M S B A M L   `   `   `  ���� O  Kmscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem   Mdm1Srt1ImportTld1%   Mdm.Srt.InputTld.MinputTldPage2   W SPresentationCore, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    8x,http://schemas.microsoft.com/winfx/2006/xaml    4sys&clr-namespace:System;assembly=mscorlib    -   $��	Tld1Page2��  � ToolTip$B  <Utility to import single or multiple items from a text file.�� N� Title$; 5Page 2 - MacroDm Tilde File Multi-Item Import Utility��$��Stretch=�$��Stretch=� J� 	AllowDrop .  N� 	KeepAlive . $
��1400��$
��1200��$
��Auto��.���� -   $��PageScrollViewer��$
��Auto=�$
��Auto=�$��Stretch=�$��Stretch=�$��	Condensed&�.��� -   $��PageGridMain�� � ShowGridLines .  $
��Auto��$
��Auto��$;  5Check off any options you want to use on this import.��$��Stretch=�$��Stretch=� . ��.  � RowDefinitions �� $��45 �$��55��$��30���� $��* ��� $��* ��� $��* ��� $��1 ��� $��1 ��� $��1 ��� $��10 ��� -    �Name$ gcRow8��$��40 �$��30��$��50�� z� SharedSizeGroup$ TldButtonRows���� -   $ gcRow9��$��* �$��45��$	��200��$ TldConsoleRows���� -   $ gcRow10��$��30 �$��30��$	��100��$ TldConsoleRows���� -   $ gcRow11��$��30 �$��30��$	��100��$ TldConsoleRows���� -   $ gcRow12��$��25 �$��5��$��35��$ TldFooterRows�� � ColumnDefinitions �� -	   $ gcCol0��$	��150��$	��150 ��� -
   $ gcCol1��$��68 ��� -   $ gcCol2��$	��115��$	��115��$	��115 ��� -   $ gcCol3��$	��138 ��� -   $ gcCol4��$	��700��$	��115��$��* ��� -   $ gcCol5��$��1��$��1��$��1 ��� -   $ gcCol6��$��1��$��1��$��1 ��� -   $ gcCol7��$��35��$��15��$��20 �$ TldBorderCols��.+��� -   $��ApplicationHeading1��$��6��$��16(�$��Center=�$��Center=�$B  <Utility to import single or multiple items from a text file.��$��3q�$��	Condensed&�$
��Left=�.��20MacroDm Import File Processing - Output Database�� -   $��DatabaseTabControl1��$��1��$��5��$��0��$��5��$	��200��$	��200��$��	Condensed&�$��10q�$
��Left=�.@��� -   $��OutputFile1Tab1��$��0q����� 	 �� Orientation$	 
Horizontal=�.+�A� $��10��$��10��
��������� Output File.���� -   $��OutputFile1Tab1StackPanel��$	 Vertical=�.+��� -   $��OutputSystemStackPanel��$	 
Horizontal=�.+��� -   $��OutputSystemLabel��$��3��$��10q�$��3q�$��25��$��90��}�.  $��12(�$��Center=�$��Center=�$��Right=�|�.  $��90��.��Output System�� -   $��OutputSystemLine��$��3��$��10q�$��3q�$��25��$��80��
 �� MaxLines$
 3�� �� 	MaxLength$	 512�� �� TextAlignment$
 Left=�$��12(�$D  >Enter the name of the system where the output database resides�� �� TextWrapping$ WrapWithOverflow=�$��Center=�}�.  " �� VerticalScrollBarVisibility$
 Auto=� �� AcceptsReturn .  $
��Left=��� -   $��OutputDatabaseLabel��$��3��$��10q�$��3q�$��25��$��90��$��Right=�$��Center=�$��12(�}�.  |�.  $��90��.��Output Database�� -   $��OutputDatabaseLine��$��3��$��10q�$��12(�$��3q�$��25��$��80��$	 512��$
 3��$
 Left=�$v  pEnter the output Database that the process will use.  Normal an ADO or ODBC datased found in the control panerl.��$ WrapWithOverflow=�$��Stretch=�$��Center=�$
 Auto=�}�.   .  �� -   $#��OutputFileGroupNameStackPanel��$	 
Horizontal=�.+��� -   $��OutputFileGroupNameLabel��$��4��$��10q�$��3q�$��12(�$��25��$��90��$��Right=�$��Center=�$��Center=�}�.  |�.  $��Right=�$��90��.��
File Group�� -   $��OutputFileGroupLine��$��4��$��10q�$��3q�$��12(�$��25��$	��200��$	 512��$
 3��$9  3Enter the output file name to use in this database.��$��Center=� � ForceCursor .   �� 	UndoLimit$ 5��}�.  $
 Auto=�$ WrapWithOverflow=�$ �� HorizontalScrollBarVisibility$
 Auto=�$
��Left=�$��Stretch=��� -   $��OutputFileStackPanel��$	 
Horizontal=� � IsSharedSizeScope .  .+��� -   $��OutputFileNameLabel��$��5��$��10q�$��3q�$��12(�$��25��$��90��$��Right=�$��Center=�$��Center=�}�.  |�.  $��90��.��Output File�� -   $��OutputFileLine��$��5��$��10q�$��3q�$��12(�$��25��$	��200��$	 512��$
 3��$9  3Enter the output file name to use in this database.��$��Center=� .  $ 5��}�.  $
 Auto=�$ WrapWithOverflow=�$
 Auto=�$
��Left=��� -    $��DbMasterServer1Tab2������ $	 
Horizontal=�.+�A� $��10��$��10��
��������� Master(Server).���� -!   $��DbServer1Tab2StackPanel��$	 Vertical=�.+��� -"   $$��DbMasterOutputSystemStackPanel��$	 
Horizontal=�.+��� -#   $��DbMasterOutputSystemLabel��$��2��$��10q�$��3q�$��12(�$��25��$��90��}�.  $��Right=�$��Center=�$��Center=�|�.  $��Right=�$��90��.��Output System�� -$   $��DbMasterOutputSystemLine��$��2��$��10q�$��3q�$��12(�$��25��$	��200��$	 512��$
 3��$
 Left=�$D  >Enter the name of the system where the output database resides��$ WrapWithOverflow=�$��Center=�}�.  $
 Auto=� .  �� -%   $&�� DbMasterOutputDatabaseStackPanel��$	 
Horizontal=�.+��� -&   $!��DbMasterOutputDatabaseLabel��$��3��$��10q�$��3q�$��11(�$��25��$��90��$��Right=�$��Center=�}�.  |�.  $��90��.��Output Database�� -'   $ ��DbMasterOutputDatabaseLine��$��3��$��10q�$��3q�$��12(�$��25��$	��200��$	 512��$
 3��$
 Left=�$v  pEnter the output Database that the process will use.  Normal an ADO or ODBC datased found in the control panerl.�� .  $ WrapWithOverflow=�$��Center=�}�.  $
 Auto=� .  �� -(   $&�� DbMasterOutputFileNameStackPanel��$	 
Horizontal=�.+��� -)   $!��DbMasterOutputFileNameLabel��$��4��$��10q�$��3q�$��12(�$��25��$��90��$��Right=�$��Center=�$��Center=�}�.  |�.  $��90��.��Output File�� -*   $��DbMasterOutputFileLine��$��4��$��10q�$��3q�$��12(�$��25��$	��200��$
 3��$9  3Enter the output file name to use in this database.��$��Center=� .  $ 5��}�.  $
 Auto=�$ WrapWithOverflow=�$
 Auto=�$
��Left=��� -+   $��DbUser1Tab3������ $	 
Horizontal=�.+�A� $��10��$��10��
��������� User.���� -,   $��DbUser1Tab3StackPanel��$	 Vertical=�.+��� --   $��DbUserIdSkackPanel��$	 
Horizontal=�.+��� -.   $��DbUserIdLabel��$��2��$��10q�$��3q�$��12(�$��25��$��90��}�.  $��Right=�$��Center=�$��Center=�|�.  $��90��.��	User Name�� -/   $��DbUserNameLine��$��2��$��10q�$��3q�$��12(�$��25��$	��200��$	 512��$
 3��$
 Left=�$D  >Enter the name of the system where the output database resides�� .  $ WrapWithOverflow=�$��Center=�}�.  $
 Auto=� .  �� -0   $��DbUserPasswordStackPanel��$	 
Horizontal=�.+��� -1   $��DbUserPasswordLabel��$��3��$��10q�$��3q�$��12(�$��25��$��90��$��Right=�$��Center=�}�.  |�.  $��90��.��User Password�� -2   $��DbUserPasswordLine��$��3��$��10q�$��3q�$��12(�$��25��$	��200��$	 512��$
 3��$
 Left=�$v  pEnter the output Database that the process will use.  Normal an ADO or ODBC datased found in the control panerl.��$ WrapWithOverflow=� � Cursor$ IBeam��$��Center=�$��Center=�}�.  $
 Auto=� .  �� -3   $,��&DbUserPasswordRequiredOptionStackPanel��$	 
Horizontal=�.+��� -4   $'��!DbUserPasswordRequiredOptionLabel��$��4��$��10q�$��3,0,0,0q�$��12(�$��25��$��90��$��Right=�$��Stretch=�}�.  |�.  $��90��$��Stretch=�.��	Passwords�� -5   $"��DbUserPasswordRequiredOption��$��4��$��10q�$��3,0,0,0q�$��12(�$��20��$	��120��}�.   .  $��Right=�$
��Left=�$��Stretch=�$��Stretch=� d� 	IsChecked$
 TrueY�$	��120��.��Password Required�� -6   $��DbSecurity1Tab4������ $	 
Horizontal=�.+�A� $��10��$��10��
��������� 
Security.���� -7   $��DbSecurity1Tab4StackPanel��$	 Vertical=�.+��� -8   $,��&DbSecurityMasterOutputSystemStackPanel��$	 
Horizontal=�.+��� -9   $'��!DbSecurityMasterOutputSystemLabel��$��2��$��10q�$��3q�$��12(�$��25��$��90��}�.  $��Right=�$��Center=�$��Center=�|�.  $��90��.��Output System�� -:   $&�� DbSecurityMasterOutputSystemLine��$��2��$��10q�$��3q�$��12(�$��25��$	��200��$	 512��$
 3�� .  }�.  $ IBeam��$
 Left=�$ WrapWithOverflow=�$D  >Enter the name of the system where the output database resides��$��Center=�$
 Auto=��� -;   $.��(DbSecurityMasterOutputDatabaseStackPanel��$	 
Horizontal=�.+��� -<   $)��#DbSecurityMasterOutputDatabaseLabel��$��3��$��10q�$��3q�$��11(�$��25��$��90��$��Right=�$��Center=�}�.  |�.  $��90��.��Output Database�� -=   $(��"DbSecurityMasterOutputDatabaseLine��$��3��$��10q�$��3q�$��12(�$��25��$	��200��$	 512��$
 3��$
 Left=�$v  pEnter the output Database that the process will use.  Normal an ADO or ODBC datased found in the control panerl.��$ WrapWithOverflow=�$ IBeam��$��Center=�$��Center=�}�.  $
 Auto=� .  �� ->   $.��(DbSecurityMasterOutputFileNameStackPanel��$	 
Horizontal=�.+��� -?   $)��#DbSecurityMasterOutputFileNameLabel��$��4��$��10q�$��3q�$��12(�$��25��$��90��$��Right=�$��Center=�$��Center=�}�.  |�.  $��90��.��Output File�� -@   $$��DbSecurityMasterOutputFileLine��$��4��$��10q�$��3q�$��12(�$��25��$	��200��$	 512��$
 3��$9  3Enter the output file name to use in this database.��$��Center=� .  $ 5��}�.  $
 Auto=�$ WrapWithOverflow=�$
 Auto=�$��Stretch=��� -A   $��OkButtonPressed��$��5q�$��5q�$��Center=�}�.  $��12(�$��Center=�$��Center=�$��60��$��20��$��8��$��3��$��	Condensed&�.��OK�� -B   $��CancelButtonPressed��$��5q�$��5q�$��12(�$��Center=�$��Center=�$��60��$��20��$��4��$��8��$��	Condensed&�.��Cancel�� -C   $��ConsoleCommandlabel��$��9��$��5,5,5,0q�$��Right=�$	��Top=�$��3q�$   ��$��27.28��.��Console Command�� -D   $��ConsoleCommandText��$��5��$��9��$��5q�$	��Top=�$��1��$��25��$	��120�� .  $
 Auto=�$ NoWrap=�$   ��$
 Auto=�$ 6000000��$

 2500�� .  �� 
AcceptsTab . $	��250���� -E   $��StatusLine1��$��10��$��1��$��5��|�.  $��5q�$��25��$	��120��$
 Auto=�$ WrapWithOverflow=�$   ��$	��150���� -F   $��StatusLine2��$��1��$��5��$��11��$��5q�|�.  $��25��$	��120��$
 Auto=�$ WrapWithOverflow=�$   ��$	��100��