   M S B A M L   `   `   `  ����L  HMusicExplorer.Old, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:MusicExplorer.OldMusicExplorer.Old  5   	   "    MusicExplorer.Old.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /oldclr-namespace:MusicExplorer.Old     5   	     � Title$  
MainWindow��5   	   $	��450��6   $	��800��6)    � 	Resources 5
      %   MusicExplorer.Old.MainViewModel   	ViewModel��%   &	          5   
   6   5       � DataContext 5      �� 5   
    �� ResourceKey$ 	ViewModel��6   .��5      �  � ColumnDefinitions 5   
   �� 5      $	��100 �6   �� 5      .+�5   
   o� ��5      �� 5      .R�5      $�  $� Orientation$ Vertical=�6-   5      .@�5      m� $��Web Explorer��5        m� CommandParameter$ webexplorer��5"       m� Command 5!      �� *NavigateCommand+5"      m� 5$      $��
Collection��5%      $ 
collection��5'       5&      �� *NavigateCommand+5'      5(      �� 5*   
   $��1��5+      ��5,      �� *	Content+5-      5.      