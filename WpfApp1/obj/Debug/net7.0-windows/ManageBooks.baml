   M S B A M L   `   `   `  ����B  >WpfApp1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:WpfApp1WpfApp1  5   	       WpfApp1.ManageBooks   5   	   W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:WpfApp1     5   	     � Title$  ManageBooks��5   	   $	��450��6   $	��800��6*   .��5	      � .+�5
   
   �� $��Manage Books��6   $��
0,44,0,304q�6'   $��0��6;   $��Center=�6H   $��Arial Rounded MT Bold)�6e   $��25(�6�   �� 5   
   $��Sirirak Books��6   $��
0,71,0,284q�6(   $��0��6<   $��Center=�6I   $��Arial Rounded MT Bold)�6f   $��15(�6�   ���	#FFFF77046�   &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ dataGrid��5         AutoGenerateColumns$ False��6%   $��18,147,18,20q�6A   $��Arial Rounded MT Bold)�6W   $��14(�6�      HeadersVisibility$ Column=�6�      GridLinesVisibility$
 None=�6�      RowHeaderWidth$ 0Q�6�      Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$
 ISBN��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6L   	   
IsReadOnly$
	 True��6V   1  +System.Windows.Controls.DataGridBoundColumn
   Binding
 63   �� *ISBN+6V     5      $ Title��6%   $ * 6N   $
	 True��6X   
 64   �� *Title+6X     5      $ Description��6%   $ * 6Z   $
	 True��6d   
 6:   �� *Description+6d     5      $ Price��6%   $ * 6N   $
	 True��6X   
 64   �� *Price+6X   4  .System.Windows.Controls.DataGridTemplateColumn  5      $
 Edit��6)   $ * 67      CellTemplate 5      �� 5      .R�5      �� -   5        $
��Edit��5   %   64   5      5        5      $ Delete��6)   $ * 69    5      �� 5       .R�5!      �� -   5        $��Delete��5!   %   66   5"      5$      5&      �� 5'   
   -   5        $��	Add Books��5'      $��651,115,0,0q�6%   $
��Left=�6Q   $	��112��6l   $��27��6x   $	��Top=�6�   $��Arial Rounded MT Bold)�6�   5(      5)      