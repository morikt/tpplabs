   M S B A M L   `   `   `  ����  
Notepad(5)    WpfNotepad.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  Блокнот��5   	   $	��450��6   $	��800��6&   $��Segoe UI)�5   	   
������� J� CommandBindings 5      )  #System.Windows.Input.CommandBinding  5	   
   -   5           Command$ ApplicationCommands.New��5	      5
        5   
   -   5        $ ApplicationCommands.Open��5      5        5   
   -   5        $ ApplicationCommands.Save��5      5      .��5      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   o� $��0��6   .@�5      m� $��	_Файл��6   .@�5      m� $��_Новый��6    m� Command$ ApplicationCommands.New��5       m� InputGestureText$ Ctrl+N��5      m� 5       $��_Открыть...��6   $ ApplicationCommands.Open��5!      $ Ctrl+O��5"      m� 5#      $��_Сохранить��6   $ ApplicationCommands.Save��5$      $ Ctrl+S��5%      �� 5&      m� 5'      -   5        $��Вы_ход��5'      6+   5(      5)      �� 5,   
   -   5         ��Name$ Editor��5,      $��1��5-       �� AcceptsReturn .  �� 
AcceptsTab . "	 �� VerticalScrollBarVisibility$
	 Auto=�50      $
 �� HorizontalScrollBarVisibility$

 Auto=�51      $��14(�52       �� TextWrapping$
 Wrap=�53      $��5q�54      55      56      