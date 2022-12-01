﻿using System.Collections.Generic;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MemoStack.Repository;
using MemoStack.UseCase;

namespace MemoStack.ViewModel;

public class MainViewModel : ObservableObject, IMainViewModel
{
    private readonly Stack<MemoModel> _stack;
    private ICommand? _createMemoCommand;
    private MemoModel _poppedMemoModel;

    public MainViewModel()
    {
        using var repository = new MemoContext();
        _stack = new StartProgramUseCase(repository).Invoke();
        PoppedMemoModel = _stack.TryPop(out var memoModel) ? memoModel : new MemoModel(string.Empty, 0);
    }

    public ICommand SaveMemoCommand { get; } = new RelayCommand<MemoModel>(model =>
    {
        if (model == null) return;
        using var repository = new MemoContext();
        var useCase = new SaveMemoUseCase(repository);
        useCase.Invoke(model);
    });

    public MemoModel PoppedMemoModel
    {
        get => _poppedMemoModel;
        private set => SetProperty(ref _poppedMemoModel, value);
    }

    public ICommand CreateMemoCommand =>
        _createMemoCommand ??= new RelayCommand(() =>
        {
            _stack.Push(PoppedMemoModel);
            using var repository = new MemoContext();
            var useCase = new CreateMemoUseCase(repository);
            PoppedMemoModel = useCase.Invoke(_stack);
        });

    public ICommand DeleteMemoCommand { get; }
}