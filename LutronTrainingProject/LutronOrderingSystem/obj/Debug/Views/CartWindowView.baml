   M S B A M L   `   `   `  ����O  KLutronOrderingSystem, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:LutronOrderingSystem.ViewsLutronOrderingSystem.Views  5   	   Q-clr-namespace:LutronOrderingSystem.ViewModelsLutronOrderingSystem.ViewModels  5   	   /    )LutronOrderingSystem.Views.CartWindowView   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   :local(clr-namespace:LutronOrderingSystem.Views     5   	   D
viewmodels-clr-namespace:LutronOrderingSystem.ViewModels     5   	     � Title$  My Cart��5
   	   $	��450��6   $	��800��6&    � 	Resources 5         CustomButtonStyle �� 
TargetType �� Property �� Value �� 
TargetType ��Name �� CornerRadius �� 
StartPoint	 �� EndPoint
 � Offset � Color �� Triggers P� Property P� Value �� 
TargetName��%^  &	        �� 5   
   " ��6*   .�5      ��  � �� 5      �� 5      " ��6&   .R�5      �� $ border��6!   
���   �$	��0.5q�5      $ 5��5      ��5      �� 5   "   $	 0,0&�66   $		 0,1&�6G   .��5   &   � $
 0Q�63   $ #b6ccb4��6>   � 5   &   $	
 0.5Q�63   $ #FFFFFF��6@   � 5   &   $
 1Q�63   $ #abf2a5��6>   5   #   ._�5      �� $��Center=�6/   $��Center=�5   '   5       5      P� 5        �@J�IsMouseOver . . �5!   "   �� $ border��6)    � �� �#e4ede46S   5"      5$      5&      5'      5(      .��5*      �  � RowDefinitions 5+   
   �� 5,      $
��Auto �6   �� 5-      $��* �6   �� 5.      $
��Auto �6   ��50   
   �� 51      $	 0,0&�6"   $		 0,1&�63   .��52      � $
 0Q�6   $ #abf2a5��6*   � 53      $	
 0.5Q�6   $ #FFFFFF��6,   � 54      $
 1Q�6   $ #DFF0D8��6*   55      .+�58   
   �� $��My Cart��6   $��20(�6#   $
��Bold"�61   $��Center=�6C   $
��0,10q�6`   &   System.Windows.Controls.DataGrid  5<   
   $��1��6      
IsReadOnly . $��0,0.333,-0.667,100.667q�62      AutoGenerateColumns .  ��6R   �� *	CartItems+   Columns 5>      0  *System.Windows.Controls.DataGridTextColumn  5?      ,  &System.Windows.Controls.DataGridColumn   Header$ Model ID��6%   1  +System.Windows.Controls.DataGridBoundColumn   Binding 67   �� *Product.ModelId+  5@      $ Model Display String��6%    6C   �� *Product.ModelDisplayString+  5A      $ Description��6%    6:   �� *Product.Description+4  .System.Windows.Controls.DataGridTemplateColumn  5B      $ Quantity��6)      CellTemplate 5C      �� 5D      .R�5E      �  � ColumnDefinitions 5F   "   �� 5G   &   $
��Auto �67   �� 5H   &   $
��Auto �67   �� 5I   &   $
��Auto �67   .+�5K   "   �� $��0��6)   $��25��69   $��-��6D   #��[  6�   $��5q�6�   ��6P   �� *DataContext.MinusCommand+ �� RelativeSource ��  �� AncestorType$ DataGridL���6�   �� 6�   �� 5L   "   $��1��6,   $
��Bold"�6V   $��Center=�6h   $��Center=�6�   ��6<   �� *
Quantity+6�   �� 5M   "   $��2��6)   $��25��69   $��+��6D   #��[  6�   $��5q�6�   ��6P   �� *DataContext.AddCommand+ �� $ DataGridL���6�   �� 6�   5N      5O      5Q        5R      $  Remove Item ��6)    5S      �� 5T      .R�5U      ��  �� Orientation$ 
Horizontal=�6)   .+�5V   "   �� $��23��6)   $��60��65   $��Remove��6@   $��5q�6Q   #��[  5X   F   ��5W   )   �� *DataContext.RemoveCommand+ �� $ DataGridL���5X   )   �� 6F   5Y      5Z      5\      5_      �� 5b   
   $��Checkout��6   $��1��6$   $��Center=�61   $��Bottom=�6N   $��359,0,359.333,33.667q�6i   $��29��5c   5   $��75��6A   #��[  6L   ��6   �� *CheckoutCommand+6L   5d      5e      