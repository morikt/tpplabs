   M S B A M L   `   `   `  ����B  >Game(4), Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:WpfGuessNumberWpfGuessNumber  5   	       WpfGuessNumber.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   .localclr-namespace:WpfGuessNumber     5   	   -   5          � Title$  Угадай число��5   	   $	��280��6   $	��380��6+   
����İ�$��Segoe UI)�5   	    � DataContext 5	         WpfGuessNumber.GameModel  5
   
   .��5      � $��15q�6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6    � ColumnDefinitions 5   
   �� 5      .+�5   
   �� $��0��6   $'��!Игра: Угадай число��6   $��18(�5      $
��Bold"�5      $��Center=�5      $��0,0,0,10q�5      
����  �5      �� 5"   
   $��1��6   $��16(�5#      $��Center=�5$      $��0,0,0,15q�5%      
���  ����5"      �� *
HintText+5&      �� 5)   
   $��2��6    �� Orientation$ 
Horizontal=�6"   $��Center=�6;   $��0,0,0,15q�6X   .+�5*      �� $	��120��6   $��Center=�5,      $��14(�5-      $��5q�5.      $��0,0,10,0q�5/      ��5+      �� *PlayerInput+ �� UpdateSourceTrigger$ PropertyChanged=�5/      �� 50      -   5        $��Проверить��50      $	��100��51      $��14(�52      
�������54      55      �� 58   
   $��3��6   $��14(�59      $��Center=�5:      
���   ���58      �� *AttemptsText+5;      �� 5>   
   -   5        $��4��5>      $��Начать заново��6   $	��140��5?      $��14(�5@      
�������$��Center=�5B      $��0,15,0,0q�5C      5D      5E      5F      